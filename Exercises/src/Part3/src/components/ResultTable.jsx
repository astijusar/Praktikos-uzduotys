import { DataGrid } from "@mui/x-data-grid";
import "./css/ResultTable.css";

export default function ResultTable({ data }) {
  const columns = [
    {
      field: "id",
      headerName: "ID",
      width: 300,
      headerClassName: "table-head",
    },
    {
      field: "sourceValue",
      headerName: "Source Value",
      width: 340,
      headerClassName: "table-head",
    },
    {
      field: "targetValue",
      headerName: "Target Value",
      width: 340,
      headerClassName: "table-head",
    },
    {
      field: "status",
      headerName: "Status",
      width: 300,
      headerClassName: "table-head",
    },
  ];

  const getStatusRowClassName = (params) => {
    const status = params.row.status;

    if (status === "unchanged") {
      return "row-unchanged";
    } else if (status === "added") {
      return "row-added";
    } else if (status === "removed") {
      return "row-removed";
    } else if (status === "modified") {
      return "row-modified";
    }

    return "";
  };

  return (
    <DataGrid
      rows={data}
      columns={columns}
      initialState={{
        pagination: {
          paginationModel: { page: 0, pageSize: 10 },
        },
      }}
      pageSizeOptions={[5, 10, 20, 30, 50, 100]}
      getRowClassName={getStatusRowClassName}
      disableColumnFilter
      sx={{
        ".MuiTablePagination-displayedRows, .MuiTablePagination-selectLabel": {
          "marginTop": "1em",
          "marginBottom": "1em",
        },
      }}
    />
  );
}
