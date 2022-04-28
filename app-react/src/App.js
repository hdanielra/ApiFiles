import React, { useEffect, useState } from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from "axios";
import MaterialTable from 'material-table';
import tableIcons from "./MaterialTableIcons";


const baseUrl = "https://localhost:44329/api/Sales";

function App() {

  const [data, setData] = useState([]);
  const [best, setBest] = useState([]);

  const requestGet=async()=>{
    await axios.get(baseUrl + "/GetData")
    .then(response=>{
      setData(response.data.data);
      setBest(response.data.mostSold.vehicle);
      // setBest(response.data.mostSold.result.vehicle);
    })
  }

  
  useEffect(() => {
      requestGet();
    },[])
  

    const columnCurrencySetting = {
      locale: 'en-CAD',
      currencyCode: 'CAD',
      minimumFractionDigits: 0
    };

  //---
  const columns=[
    { title: "Deal Number", field: "dealNumber", type: "numeric" },
    { title: "Customer Name", field: "customerName" },
    { title: "Dealership Name", field: "dealershipName" },
    { title: "Vehicle", field: "vehicle" },
    { title: "Price", field: "price", type: "currency", 
              currencySettings: columnCurrencySetting,
              render: row => 
                <span>CAD{ 
                  //currency format
                  Intl.NumberFormat('en-CA', { currency: 'CAD', style: 'currency', }).format(row["price"] ) // 'CA$ 100.00'   
                  }
                </span>  
    },
    { title: "Date", field: "date" }
  ];


  const [file, setFile] = useState(null);

  const uploadFile = e => {
    setFile(e);
  }


  const saveFile=async()=>{
    const f =  new FormData();

    f.append("file", file[0]);


    await axios.post(baseUrl + "/UploadFileCsv", f)
    .then(
      response => {
        console.log(response.data);
        requestGet();
      }).catch(error => {
        console.log(error);
      });

  }



  return (
    <div className="App">

      <div style={{padding:"10px"}}>
        <h1  className='display-4' ><span style={{color: "gray"}}> Sales of Vehicles - File Upload </span></h1>
      </div>

      <div style={{padding:"10px", backgroundColor:"whitesmoke"}}> 
        <br/><br/>
        <input type='file' name='files'  onChange={(e)=>uploadFile(e.target.files)}/>
        <br/><br/>
        <button className='btn btn-primary' onClick={()=>saveFile()}>Upload</button>
      </div>


      <div style={{padding: "20px"}}>
        <h3 className='display-6 text-left'  style={{backgroundColor: "lightblue"}}>
          The most frequently sold vehicle : <span style={{color: "Tomato"}}>{ best } </span>
        </h3>         
      </div>

      

      <div>
        <MaterialTable 
          // tableRef={this.tableRef}
          title="List of Sales"
          icons={tableIcons} 
          columns = {columns}
          data={data}
          // actions={[
          //   {
          //     icon: tableIcons.Delete,
          //     tooltip: "Delete User",
          //     onClick: (event, rowData) => alert("You want to delete " + rowData.name),
          //   },
          //   {
          //     icon: tableIcons.Add,
          //     tooltip: "Add User",
          //     isFreeAction: true,
          //     onClick: (event) => alert("You want to add a new row"),
          //   },
          // ]}
          options={{
            exportButton: true,
            grouping: true,
            sorting: true,
            actionsColumnIndex:-1
           }}
        />
      </div>
    
    </div>
  );
}

export default App;
