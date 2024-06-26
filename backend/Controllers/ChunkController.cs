﻿using backend.Data;
using backend.Models;
using backend.Models.Authentication;
using backend.Modules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChunkController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string _tempFolder;
        private readonly BitChunk _client;

        public ChunkController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            _tempFolder = Directory.GetCurrentDirectory() + "/temp";
            _client = new BitChunk();
        }

        [HttpPost("{FileName}")]
        public async Task<IActionResult> Post(string FileName, EncryptionKeyDTO encryptionKeyDto)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            if (encryptionKeyDto.ChunksPassword == null)
                return BadRequest(new Response { Status = "Error", Message = "You need to supply the chunks encryption key" });

            string filePath = Path.Combine(_tempFolder, FileName);
            if(System.IO.File.Exists(filePath))
            {
                try
                {
                    _client.SetFile(filePath);
                    _client.Shred();
                    _client.EncryptChunks(encryptionKeyDto.ChunksPassword);
                    _client.GenerateRef();

                    var chunksNames = new List<string>();
                    foreach(var chunks in _client.ChunksMetaData)
                    {
                        chunksNames.Add(chunks.Split("/")[^1]);
                    }


                    stopwatch.Stop();
                    if(encryptionKeyDto.UserId != null)
                    {
                        var model = await _userManager.FindByIdAsync(encryptionKeyDto.UserId);
                        if (model != null)
                        {
                            model.CurrentQuota += (uint)_client.InputFileSize;
                            await _userManager.UpdateAsync(model);
                        }
                        var newLog = new Log()
                        {
                            RefFileName = _client.RefFile.Split("/")[^1],
                            OriFileName = _client.InputFileName.Split("/")[^1],
                            FileSize = _client.InputFileSize,
                            OperationTimes = stopwatch.ElapsedMilliseconds,
                            TimeStamp = DateTime.Now,
                            UserId = encryptionKeyDto.UserId
                        };

                        await _db.Logs.AddAsync(newLog);
                        await _db.SaveChangesAsync();
                    }

                    return Ok(new { 
                        Status = "Success", 
                        Message = $"{_client.RefFile.Split("/")[^1]} has been generated", 
                        ReferenceFile = _client.RefFile.Split("/")[^1], 
                        FileName = _client.InputFileName.Split("/")[^1],
                        FileSize = _client.InputFileSize,
                        ChunkSize = _client.ChunkSize,
                        ChunksCount = chunksNames.Count,
                        ChunksMetaData = chunksNames
                    });
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = e.Message });
                }
            }
            return NotFound(new Response { Status = "Error", Message = $"{FileName} was not found" });
        }

        [HttpPost("[action]/{RefFile}")]
        public IActionResult Encrypt(string RefFile, EncryptionKeyDTO encryptionKeyDTO)
        {
            try
            {
                _client.EncryptRef(Path.Combine(_tempFolder, RefFile), encryptionKeyDTO.RefPassword);
                return Ok(new { Status = "Success", Message = $"{_client.RefFile.Split("/")[^1]} has been encrypted with AES-256", ReferenceFile = _client.RefFile.Split("/")[^1] });
            } catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = e.Message });
            }
        }

        [HttpPost("[action]/{RefFile}")]
        public IActionResult Decrypt(string RefFile, EncryptionKeyDTO encryptionKeyDTO)
        {
            try
            {
                _client.DecryptRef(Path.Combine(_tempFolder, RefFile), encryptionKeyDTO.RefPassword);
                return Ok(new { Status = "Success", Message = $"{_client.RefFile.Split("/")[^1]} has been decrypted", ReferenceFile = _client.RefFile.Split("/")[^1] });
            } catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = e.Message });
            }
        }

        [HttpGet("{RefFile}")]
        public IActionResult RefFile(string RefFile)
        {
            string filePath = Path.Combine(_tempFolder, RefFile);
            if (System.IO.File.Exists(filePath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                System.IO.File.Delete(filePath);
                return File(fileBytes, "application/force-download", RefFile);
            }
            return NotFound(new Response { Status = "Error", Message = $"{RefFile} was not found on the server!" });
        }

        [HttpPost("[action]")]
        public IActionResult UploadRefFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                var pathToSave = _tempFolder;
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(_tempFolder, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new { Status = "Success", UploadedRefFile = fileName });

                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost("[action]/{RefFile}")]
        public IActionResult Reassemble(string RefFile, EncryptionKeyDTO encryptionKeyDTO)
        {
            try {
                string filePath = Path.Combine(_tempFolder, RefFile);

                
                _client.ParseRef(filePath);
                System.IO.File.Delete(filePath);
                _client.ReassembleFile(encryptionKeyDTO.ChunksPassword);

                if (System.IO.File.Exists(_client.ReassembledFileName))
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(_client.ReassembledFileName);
                    System.IO.File.Delete(_client.ReassembledFileName);
                    return File(fileBytes, "application/force-download", _client.ReassembledFileName.Split("/")[^1]);
                }

                return NotFound(new Response { Status = "Error", Message = $"{_client.ReassembledFileName.Split("/")[^1]} was not found on the server!" });

            } catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = e.Message });
            }
        }
    }
}
