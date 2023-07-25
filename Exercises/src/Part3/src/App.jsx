import { useState, useEffect } from "react";
import FileUploadInput from "./components/FileUploadInput";
import Navbar from "./components/navbar";
import "./css/App.css";

export default function App() {
  const [selectedFiles, setSelectedFiles] = useState({
    sourceFile: null,
    targetFile: null
  })

  function handleFileSelect(fileInputName, file) {
    setSelectedFiles((prevSelectedFiles) => ({
      ...prevSelectedFiles,
      [fileInputName]: file
    }))
  }

  useEffect(() => {
    if (selectedFiles.sourceFile && selectedFiles.targetFile) {
      console.log("Making POST request")
    }
  }, [selectedFiles])

  return (
    <div>
      <Navbar />
      <div className="container" style={{ marginTop: "6vh" }}>
        <div className="row">
          <div className="col d-flex justify-content-center">
            <h1 className="display-1 fw-normal">CFG Compare Tool</h1>
          </div>
        </div>
        <div className="row">
          <div className="col d-flex justify-content-center">
            <p className="lead subheading text-center">Easily compare two configuration files</p>
          </div>
        </div>
        <div className="row d-flex justify-content-center mt-5">
          <div className="col-lg-5">
            <FileUploadInput
              fileInputName="sourceFile"
              onFileSelect={handleFileSelect} 
              label="Source File"
            />
          </div>
          <div className="col-lg-5 mt-3 mt-lg-0">
            <FileUploadInput
              fileInputName="targetFile"
              onFileSelect={handleFileSelect} 
              label="Target File"
            />
          </div>
        </div>
      </div>
    </div>
  );
}
