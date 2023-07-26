import { BarLoader } from "react-spinners";

export default function LoadingSection({ isLoading }) {
  return (
    isLoading && (
      <div className="row mt-3">
        <div className="col-12">
          <BarLoader loading={isLoading} className="w-100" color="#1e3d71" />
        </div>
      </div>
    )
  );
}
