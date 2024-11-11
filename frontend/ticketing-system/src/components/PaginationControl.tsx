import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
interface PaginationControlProps {
  itemsPerPage: number;
  setItemsPerPage: (itemsPerPage: number) => void;
  currentPage: number;
  setCurrentPage: (currentPage: number) => void;
  totalItems: number;
}

const PaginationControl: React.FC<PaginationControlProps> = ({
  itemsPerPage,
  setItemsPerPage,
  currentPage,
  setCurrentPage,
  totalItems,
}) => {
  const totalPages = Math.ceil(totalItems / itemsPerPage);
  const adjustedCurrentPage = Math.min(currentPage, totalPages);

  const handlePageChange = (newPage: number) => {
    if (newPage >= 1 && newPage <= totalPages) {
      setCurrentPage(newPage);
    }
  };

  const handleItemsPerPageChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    const newItemsPerPage = Number(event.target.value);
    setItemsPerPage(newItemsPerPage);
    setCurrentPage(1);
  };
  const pageNumbers = [];
  for (let i = 1; i <= totalPages; i++) {
    pageNumbers.push(i);
  }

  return (
    <div className="pagination-container mt-3">
        <div className="pagination-section">
        <nav>
        <ul className="pagination justify-content-center">
          <li className={`page-item ${adjustedCurrentPage === 1 ? 'disabled' : ''}`}>
            <button
              className="page-link"
              onClick={() => handlePageChange(adjustedCurrentPage - 1)}
            >
              Previous
            </button>
          </li>
          {pageNumbers.map((page) => (
            <li
              key={page}
              className={`page-item ${page === adjustedCurrentPage ? 'active' : ''}`}
            >
              <button
                className="page-link"
                onClick={() => handlePageChange(page)}
              >
                {page}
              </button>
            </li>
          ))}

          <li className={`page-item ${adjustedCurrentPage === totalPages ? 'disabled' : ''}`}>
            <button
              className="page-link"
              onClick={() => handlePageChange(adjustedCurrentPage + 1)}
            >
              Next
            </button>
          </li>
        </ul>
      </nav>

      <div className="col-2 d-flex justify-content-end mt-3">
        <select
          id="items-per-page"
          className="form-select"
          value={itemsPerPage}
          onChange={handleItemsPerPageChange}
        >
          <option value={10}>10</option>
          <option value={20}>20</option>
          <option value={50}>50</option>
        </select>
      </div>
        </div>
    </div>
  );
};

export default PaginationControl;
