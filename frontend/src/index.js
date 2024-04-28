import axios from "axios"
import React from "react"
import ReactDOM from "react-dom"
import App from "./App"
import "./index.css"

axios.defaults.baseURL = "http://bitchunk.younes.ai"
axios.defaults.headers.post["Content-Type"] = "application/json"

ReactDOM.render(<App />, document.getElementById("root"))
