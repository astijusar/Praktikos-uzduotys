import FileUploadInput from "./FileUploadInput";
import FileInformationCard from "./FileInformationCard";

export default function FileUploadSection({
  comparisonResult,
  sourceFileData,
  targetFileData,
  handleFileSelect,
}) {
  return (
    <div className="row d-flex justify-content-center mt-5">
      <div className="col-lg-6">
        {comparisonResult === null ? (
          <FileUploadInput
            fileInputName="sourceFile"
            onFileSelect={handleFileSelect}
            label="Source File"
          />
        ) : (
          <FileInformationCard file={sourceFileData} label="Source File: " />
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
          <FileInformationCard file={targetFileData} label="Target File: " />
        )}
      </div>
    </div>
  );
}
