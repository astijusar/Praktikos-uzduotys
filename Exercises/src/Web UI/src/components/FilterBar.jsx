import { useState } from "react";
import "./css/FilterBar.css";

export default function FilterBar({
  onSubmit,
  isActiveSubmitButton,
  statusCount,
  handleReset,
}) {
  const [searchValue, setSearchValue] = useState("");
  const [filters, setFilters] = useState({
    unchanged: false,
    modified: false,
    added: false,
    removed: false,
  });

  function handleSearchChange(e) {
    setSearchValue(e.target.value);
  }

  function handleFilterChange(e) {
    const { name, checked } = e.target;
    setFilters((prevFilters) => ({ ...prevFilters, [name]: checked }));
  }

  function handleSubmit() {
    const filterString = Object.entries(filters)
      .filter(([filter, checked]) => checked)
      .map(([filter, checked]) => filter)
      .join(",");

    onSubmit(filterString, searchValue);
  }

  function onReset() {
    setFilters({
      unchanged: false,
      modified: false,
      added: false,
      removed: false,
    });

    handleReset();
  }

  return (
    <div className="container">
      <div className="row filter-bar">
        <div className="col-lg-8">
          <div className="form-check form-check-inline">
            <input
              className="form-check-input mt-2"
              type="checkbox"
              id="checkboxUnchanged"
              name="unchanged"
              value="unchanged"
              onChange={handleFilterChange}
              checked={filters.unchanged}
            />
            <label className="form-check-label" htmlFor="checkboxUnchanged">
              Unchanged{" "}
              <span className="fw-bolder">
                {statusCount.Unchanged !== 0 && `(${statusCount.Unchanged})`}
              </span>
            </label>
          </div>
          <div className="form-check form-check-inline">
            <input
              className="form-check-input mt-2"
              type="checkbox"
              id="checkboxModified"
              name="modified"
              value="modified"
              onChange={handleFilterChange}
              checked={filters.modified}
            />
            <label className="form-check-label" htmlFor="checkboxModified">
              Modified{" "}
              <span className="fw-bolder">
                {statusCount.Modified !== 0 && `(${statusCount.Modified})`}
              </span>
            </label>
          </div>
          <div className="form-check form-check-inline">
            <input
              className="form-check-input mt-2"
              type="checkbox"
              id="checkboxRemoved"
              name="removed"
              value="removed"
              onChange={handleFilterChange}
              checked={filters.removed}
            />
            <label className="form-check-label" htmlFor="checkboxRemoved">
              Removed{" "}
              <span className="fw-bolder">
                {statusCount.Removed !== 0 && `(${statusCount.Removed})`}
              </span>
            </label>
          </div>
          <div className="form-check form-check-inline">
            <input
              className="form-check-input mt-2"
              type="checkbox"
              id="checkboxAdded"
              name="added"
              value="added"
              onChange={handleFilterChange}
              checked={filters.added}
            />
            <label className="form-check-label" htmlFor="checkboxAdded">
              Added{" "}
              <span className="fw-bolder">
                {statusCount.Added !== 0 && `(${statusCount.Added})`}
              </span>
            </label>
          </div>
        </div>
        <div className="col-lg-4 mt-2 mt-lg-0 d-flex justify-content-end">
          <input
            type="text"
            className="form-control me-2"
            placeholder="Search by id"
            value={searchValue}
            onChange={handleSearchChange}
          />
          <button
            type="button"
            className="filter-bar-btn me-2"
            onClick={handleSubmit}
            disabled={!isActiveSubmitButton}
          >
            Compare
          </button>
          <button
            type="button"
            className="btn btn-outline-danger"
            onClick={onReset}
          >
            Reset
          </button>
        </div>
      </div>
    </div>
  );
}
