import { useState } from "react";
import axios from "axios";
import FileUploadInput from "./components/FileUploadInput";
import Navbar from "./components/navbar";
import "./css/App.css";
import FilterBar from "./components/FilterBar";

export default function App() {
  const [sourceFileData, setSourceFileData] = useState(null);
  const [targetFileData, setTargetFileData] = useState(null);
  const [comparisonResult, setComparisonResult] = useState(null);
  const [selectedFiles, setSelectedFiles] = useState({
    sourceFile: null,
    targetFile: null,
  });

  function handleFileSelect(fileInputName, file) {
    setSelectedFiles((prevSelectedFiles) => ({
      ...prevSelectedFiles,
      [fileInputName]: file,
    }));
  }

  async function handleSubmit(filters, search) {
    const formData = new FormData();
    formData.append("sourceFile", selectedFiles.sourceFile);
    formData.append("targetFile", selectedFiles.targetFile);

    const params = {
      ID: search,
      ResultStatus: filters,
    };

    await axios
      .post(import.meta.env.VITE_API_URL + "/api/FileComparison", formData, {
        params: {
          ID: search,
          ResultStatus: filters,
        },
        headers: {
          "Content-Type": "multipart/form-data",
        },
      })
      .then((response) => {
        console.log(response.data);
        setSourceFileData(response.data.sourceFile);
        setTargetFileData(response.data.targetFile);
        setComparisonResult(response.data.comparisonResult);
      })
      .catch((error) => {
        console.log(error);
      });
  }

  const showFilterBar =
    selectedFiles.sourceFile !== null && selectedFiles.targetFile !== null;

  return (
    <div>
      <Navbar />
      <div className="container" style={{ marginTop: "6vh" }}>
        <div className="row">
          <div className="col d-flex justify-content-center">
            <h1 className="display-1 fw-normal">.CFG Comparison Tool</h1>
          </div>
        </div>
        <div className="row">
          <div className="col d-flex justify-content-center">
            <p className="lead subheading text-center">
              Easily compare two configuration files
            </p>
          </div>
        </div>
        <div className="row d-flex justify-content-center mt-5">
          <div className="col-lg-6">
            <FileUploadInput
              fileInputName="sourceFile"
              onFileSelect={handleFileSelect}
              label="Source File"
            />
          </div>
          <div className="col-lg-6 mt-3 mt-lg-0">
            <FileUploadInput
              fileInputName="targetFile"
              onFileSelect={handleFileSelect}
              label="Target File"
            />
          </div>
        </div>
        <div className="row d-flex justify-content-center mt-3">
          <div className="col-12">
            {showFilterBar && <FilterBar onSubmit={handleSubmit} />}
          </div>
        </div>
      </div>
    </div>
  );
}
