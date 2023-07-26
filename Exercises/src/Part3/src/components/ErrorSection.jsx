export default function ErrorSection({ isError }) {
  return (
    isError && (
      <div className="alert alert-danger mt-3" role="alert">
        Something went wrong. Please try again later!
      </div>
    )
  );
}
