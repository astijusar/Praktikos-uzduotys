import { useEffect, useState } from "react";
import axios from "axios";
import Navbar from "./components/Navbar";
import "./css/App.css";
import FileUploadSection from "./components/FileUploadSection";
import FilterBar from "./components/FilterBar";
import ErrorSection from "./components/ErrorSection";
import LoadingSection from "./components/LoadingSection";
import ResultSection from "./components/ResultSection";

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
  const [statusCount, setStatusCount] = useState({
    Unchanged: 0,
    Modified: 0,
    Added: 0,
    Removed: 0,
  });

  function handleFileSelect(fileInputName, file) {
    setSelectedFiles((prevSelectedFiles) => ({
      ...prevSelectedFiles,
      [fileInputName]: file,
    }));
  }

  function handleReset() {
    setSelectedFiles({
      sourceFile: null,
      targetFile: null,
    });
    setStatusCount({
      Unchanged: 0,
      Modified: 0,
      Added: 0,
      Removed: 0,
    });
    setComparisonResult(null);
    setIsActiveSubmitButton(false);
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

        const counts = {
          Unchanged: 0,
          Modified: 0,
          Added: 0,
          Removed: 0,
        };
    
        response.data.comparisonResult.forEach((item) => {
          switch (item.status) {
            case "Unchanged":
              counts.Unchanged++;
              break;
            case "Modified":
              counts.Modified++;
              break;
            case "Added":
              counts.Added++;
              break;
            case "Removed":
              counts.Removed++;
              break;
            default:
              break;
          }
        });
    
        setStatusCount(counts);
        console.log(counts);
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
    } else {
      setIsActiveSubmitButton(false);
    }
  }, [selectedFiles]);

  return (
    <div>
      <Navbar />
      <div className="container" style={{ marginTop: "6vh" }}>
        <div className="row">
          <div className="col d-flex justify-content-center">
            <h1 className="display-1 fw-normal text-center">
              .CFG File Comparison Tool
            </h1>
          </div>
        </div>
        <div className="row">
          <div className="col d-flex justify-content-center">
            <p className="lead subheading text-center">
              Easily compare two configuration files
            </p>
          </div>
        </div>
        <FileUploadSection
          comparisonResult={comparisonResult}
          sourceFileData={sourceFileData}
          targetFileData={targetFileData}
          handleFileSelect={handleFileSelect}
        />
        <ErrorSection isError={isError} />
        <LoadingSection isLoading={isLoading} />
        <div className="row d-flex justify-content-center mt-4 mt-lg-3">
          <div className="col-12">
            <FilterBar
              onSubmit={handleSubmit}
              isActiveSubmitButton={isActiveSubmitButton}
              statusCount={statusCount}
              handleReset={handleReset}
            />
          </div>
        </div>
        <ResultSection comparisonResult={comparisonResult} />
      </div>
    </div>
  );
}
