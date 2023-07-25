import { useRef, useState } from "react";
import { Upload, FileEarmark } from "react-bootstrap-icons";
import "./css/FileUploadInput.css";

export default function FileUploadInput({
  fileInputName,
  onFileSelect,
  label,
}) {
  const fileInputRef = useRef(null);
  const [selectedFileName, setSelectedFileName] = useState("");
  const allowedExtensions = /(\.cfg)$/i;
  const maxSizeInBytes = 100 * 1024;

  const handleFileSelect = () => {
    fileInputRef.current.click();
  };

  const handleFileChange = (event) => {
    const selectedFile = event.target.files[0];

    if (!allowedExtensions.test(selectedFile.name)) {
      alert("Please select a file with the .cfg extension.");
      fileInputRef.current.value = null;
      return;
    }

    if (selectedFile.size > maxSizeInBytes) {
      alert("File size exceeds the allowed limit of 100KB.");
      fileInputRef.current.value = null;
      return;
    }

    setSelectedFileName(selectedFile.name);
    onFileSelect(fileInputName, selectedFile);
  };

  return (
    <div className="container">
      <label htmlFor={`fileInputName-${fileInputName}`}>
        {<FileEarmark className="mb-1 me-2" />}
        <span className="fw-bolder">{label}</span>
      </label>
      <div className="file-upload-box" onClick={handleFileSelect}>
        {selectedFileName ? (
          <div>
            File name: <span className="file-name">{selectedFileName}</span>
          </div>
        ) : (
          <div>{<Upload className="mb-1" />} Click to choose a file</div>
        )}
      </div>
      <input
        type="file"
        ref={fileInputRef}
        style={{ display: "none" }}
        onChange={handleFileChange}
        id={`fileInput-${fileInputName}`}
      />
    </div>
  );
}
