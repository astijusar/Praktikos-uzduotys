import ResultTable from "./ResultTable";

export default function ResultSection({ comparisonResult }) {
  return (
    comparisonResult && (
      <div className="row mb-5 mt-4 mt-lg-3">
        <div className="col-12">
          <ResultTable data={comparisonResult} />
        </div>
      </div>
    )
  );
}
