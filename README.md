<h1 align="center">
  <br>
<a href="https://bit-chunk.netlify.app" target="_blank"><img src="https://raw.githubusercontent.com/younes-alturkey/bitchunk/main/frontend/src/assets/img/logo.png" alt="Tuwaiq Logo" width="80"/></a>
  <br>

<a href="https://bit-chunk.netlify.app" target="_blank">Bitchunk™: Secure File Sharing and Storage</a>

</h1>
<h4 align="center">Accurate data sharding, random distribution, and encryption service.</h4>

<p align="center">
  <a href="#key-features">Key Features</a> •
  <a href="#how-to-use">How To Use</a> •
   <a href="#deploy">Deploy</a> •
  <a href="#tech-stack">Tech Stack</a> •
  <a href="#license">License</a> •
  <a href="https://bit-chunk.netlify.app">Live</a>
</p>

<p align="center">
  <a href="https://bit-chunk.netlify.app" target="_blank"><img src="https://raw.githubusercontent.com/younes-alturkey/bitchunk.co/main/frontend/src/assets/img/demo.gif" alt="demo"/></a>
</p>

<br/>

<div id="key-features">

## Key Features - المزايا

- **Simple**, **Modern**, **Easy-To-Use** user interface
- Original file is immediately deleted after sharding
- Small and portable reference file that is **easy to share**
- **Password-locked** assembly point
- Files may only be assembled once for enhanced security
- Shard somewhere, **assemble anywhere**
- AES-256 Encryption

</div>
<br/>

<div id="how-to-use">

## How To Use - طريقة الإستخدام

From your command line:

#### # Clone this repository

```bash
git clone https://github.com/younes-alturkey/bitchunk.co.git
```

#### # Go into the frontend directory

```bash
cd bitchunk.co/frontend
```

#### # Install the dependencies

```bash
npm install
```

#### # Run the app

```bash
npm start
```

Note: the service is in its first version, so expect some bugs.

</div>

<br/>

<div id="deploy">

## Deploy - نشر

```
sudo apt install docker.io
```

```
sudo systemctl start docker
```

```
sudo systemctl enable docker
```

```
sudo apt-get install ufw
```

```
sudo ufw enable
```

```
sudo ufw allow ssh
```

```
sudo ufw allow 5000
```

```
sudo docker build -t backend .
```

```
docker run -d --name backend --restart always -p 5000:5000 backend
```

</div>

<br/>
<div id="tech-stack">

## Tech Stack - التكنولوجيا المستخدمة

### <a href="https://bitchunk-api.azurewebsites.net/swagger/index.html" target="_blank">Backend</a> - الخلفية

- .NET Core 5 Web API with 5 Controllers
- Entity Framework - Code First Approach
- Microsoft SQL Database with 8 Tables

### <a href="https://bit-chunk.netlify.app" target="_blank">Frontend</a> - الواجهة

- React.js 17.0.2 with 10 Views

### Deployment - السيرفرات

- Backend hosted at Azure App Service S3 Plan
- Database hosted at Azure SQL Database
- Fronted hosted at Netlify free service

</div>

<br/>

## Wireframes - التصاميم

<a href="https://github.com/younes-alturkey/bitchunk.co/blob/main/frontend/bitchunk-wireframes.pdf" target="_blank">bitchunk-wireframes.pdf</a>

<br/>

## Google Lighthouse Report - إختبار الجودة من جوجل

<p align="center">
<a href="https://googlechrome.github.io/lighthouse/viewer/?psiurl=https%3A%2F%2Fbitchunk.co"><img src="https://github.com/younes-alturkey/bitchunk.co/blob/main/frontend/src/assets/img/lighthouse-report.png" alt="Quality Check"/></a>
</p>

<br/>

## Unit Tests - الإختبارات

<p align="center">
<img src="https://raw.githubusercontent.com/younes-alturkey/bitchunk.co/main/backend-tests/tests-results.png" alt="Unit Tests Screenshot"/>
</p>

<div id="license">

<br/>

## License - الرخصة

[Open Source License ➝ MIT](https://github.com/younes-alturkey/bitchunk.co/blob/main/LICENSE.md)

</div>

<br/>

## Developer - المطور

> Younes Alturkey - يونس التركي

> GitHub: [@younes-alturkey](https://github.com/younes-alturkey) &nbsp;&middot;&nbsp;
> Website: [younesalturkey.sa](https://younesalturkey.sa) &nbsp;&middot;&nbsp;
> LinkedIn: [younes-alturkey](https://www.linkedin.com/in/younes-alturkey/)

</div>
