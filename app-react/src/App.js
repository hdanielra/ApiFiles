import React, { useEffect, useState } from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from "axios";
import MaterialTable from 'material-table';
// import { AddBox, ArrowDownward } from "@material-ui/icons";
// import AddBox from "@material-ui/icons/AddBox";
// import ArrowDownward from "@material-ui/icons/ArrowDownward";
import tableIcons from "./MaterialTableIcons";


const baseUrl = "https://localhost:44329/api/Sales";

function App() {
// class App extends Component {


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
                  // number.toLocaleString('en-CA', { style: 'currency', currency: 'CAD' });
                  }
                </span>  
    },
    { title: "Date", field: "date" }
  ];


  // let data = []
    // let data = [
    //     {
    //         dealNumber: 5469,
    //         customerName: "Milli Fulton",
    //         dealershipName: "Sun of Saskatoon",
    //         vehicle: "2017 Ferrari 488 Spider",
    //         price: 429987,
    //         date: "2018-06-19T00:00:00"
    //     },
    //     {
    //         dealNumber: 5132,
    //         customerName: "Rahima Skinner",
    //         dealershipName: "Seven Star Dealership",
    //         vehicle: "2009 Lamborghini Gallardo Carbon Fiber LP-560",
    //         price: 169900,
    //         date: "2018-01-14T00:00:00"
    //     }
    // ]
  // ---


  const [file, setFile] = useState(null);
  // const [tableRef, setTableRef] = useState(null);
  // const tableRef = React.createRef();

  const uploadFile = e=> {
    setFile(e);
  }


  const saveFile=async()=>{
    const f =  new FormData();

    f.append("file", file[0]);
    // for (let index = 0; index < file.length; index++) {
    //   f.append("file", file[index]);
      
    // }

    // await axios.post("https://localhost:44367/api/Files/PostFiles", f)
    // await axios.post("https://localhost:44367/api/Files/PostFiles", 
    //                 f, 
    //                 {headers: {'Content-Type':'multipart/form-data'}}
    //                 )

    


    // await axios.post("https://localhost:44329/api/Sales/UploadFileCsv", f)
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
        The most frequently sold vehicle : <span style={{color: "Tomato"}}>{ best } </span></h3>         
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
