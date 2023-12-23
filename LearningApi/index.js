const express = require('express');

const bodyparser = require('body-parser');

const cors = require('cors');
const mysql=require('mysql');

const app = express();

app.use(cors());
app.use(bodyparser.json());

const db = mysql.createConnection({  

    host: 'localhost',
    
    user:'root',
    
    password:'',
    
    database: 'userinfo',
    
    port: 3306
    
    });
    
    db.connect((err)=>{
    
    if(err) throw err;
    
    console.log('MySql Connected');
    
    })

app.listen(3000, ()=>{

console.log("Server is runing on 3000 PORT, Testycodeiz");
})