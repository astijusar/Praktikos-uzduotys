import { useEffect, useState } from "react";
import axios from "axios";
import FileUploadInput from "./components/FileUploadInput";
import Navbar from "./components/navbar";
import "./css/App.css";
import FilterBar from "./components/FilterBar";
import FileInformationCard from "./components/FileInformationCard";
import { BarLoader } from "react-spinners";
import ResultTable from "./components/ResultTable";

export default function App() {
  const [isError, setIsError] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [isActiveSubmitButton, setIsActiveSubmitButton] = useState(false);
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

    setIsError(false);
    setIsLoading(true);
    setIsActiveSubmitButton(false);

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
        setIsError(true);
      });

      setIsLoading(false);
      setIsActiveSubmitButton(true);
  }

  useEffect(() => {
    if (selectedFiles.sourceFile && selectedFiles.targetFile) {
      setIsActiveSubmitButton(true);
    }
  }, [selectedFiles])

  return (
    <div>
      <Navbar />
      <div className="container" style={{ marginTop: "6vh" }}>
        <div className="row">
          <div className="col d-flex justify-content-center">
            <h1 className="display-1 fw-normal text-center">.CFG File Comparison Tool</h1>
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
            {comparisonResult === null ? (
              <FileUploadInput
                fileInputName="sourceFile"
                onFileSelect={handleFileSelect}
                label="Source File"
              />
            ) : (
              <FileInformationCard
                file={sourceFileData}
                label="Source File: "
              />
            )}
          </div>
          <div className="col-lg-6 mt-3 mt-lg-0">
            {comparisonResult === null ? (
              <FileUploadInput
                fileInputName="targetFile"
                onFileSelect={handleFileSelect}
                label="Target File"
              />
            ) : (
              <FileInformationCard
                file={targetFileData}
                label="Target File: "
              />
            )}
          </div>
        </div>
        {isError && (
          <div className="alert alert-danger mt-3" role="alert">
            Something went wrong. Please try again later!
          </div>
        )}
        {isLoading && (
          <div className="row mt-3">
            <div className="col-12">
              <BarLoader 
                loading={isLoading}
                className="w-100"
                color="#1e3d71"
              />
            </div>
          </div>
        )}
        <div className="row d-flex justify-content-center mt-4 mt-lg-3">
          <div className="col-12">
            <FilterBar
              onSubmit={handleSubmit}
              isActiveSubmitButton={isActiveSubmitButton}
            />
          </div>
        </div>
        {comparisonResult && (
          <div className="row mb-5 mt-4 mt-lg-3">
            <div className="col-12">
              <ResultTable 
                data={comparisonResult}
              />
            </div>
          </div>
        )}
      </div>
    </div>
  );
}
