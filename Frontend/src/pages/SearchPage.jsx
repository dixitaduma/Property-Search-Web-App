import React from "react";
import { useState, useEffect } from "react";
import { Link, useLocation } from "react-router-dom";
import { getPropertyWithFilters } from "../services/api";

const SearchPage = () => {
  const location = useLocation();
  const [properties, setProperties] = useState([]);
  const [loading, setLoading] = useState(false);
  const [successMessage, setSuccessMessage] = useState("");
  const [error, setError] = useState(null);
  const [filters, setFilters] = useState({
    type: "",
    minPrice: "",
    maxPrice: "",
  });
  const [appliedFilters, setAppliedFilters] = useState({
    type: "",
    minPrice: "",
    maxPrice: "",
  });
  const [currentPage, setCurrentPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalCount, setTotalCount] = useState(null);
  const [hasMore, setHasMore] = useState(null);
  useEffect(() => {
    if (location.state?.message) {
      setSuccessMessage(location.state.message);
      window.history.replaceState({}, document.title);
    }
    fetchProperties(appliedFilters, currentPage, pageSize);
  }, [location.state]);

  useEffect(() => {
    if (!successMessage) return;
    const timer = setTimeout(() => setSuccessMessage(""), 3000);
    return () => clearTimeout(timer);
  }, [successMessage]);

  const fetchProperties = async (currentFilters, pageOverride, pageSizeOverride) => {
    setLoading(true);
    setError(null);
    try {
      const params = Object.fromEntries(
        Object.entries(currentFilters || {}).filter(
          ([, v]) => v !== "" && v !== null && v !== undefined
        )
      );
      const pageParam = pageOverride ?? currentPage;
      const sizeParam = pageSizeOverride ?? pageSize;
      const response = await getPropertyWithFilters({ ...params, pageNumber: pageParam, pageSize: sizeParam });
      if (Array.isArray(response.properties)) {
        const items = response.properties;
        setProperties(items);
        const total = response.totalCount ?? response.total ?? response.count ?? response.totalRecords ?? null;
        if (typeof total === "number") {
          setTotalCount(total);
          setHasMore(pageParam < Math.ceil(total / sizeParam));
        } else {
          setTotalCount(null);
          setHasMore(items.length === sizeParam);
        }
      } else if (response && Array.isArray(response.data)) {
        const items = response.data;
        setProperties(items);
        const total = response.totalCount ?? response.total ?? response.count ?? response.totalRecords ?? null;
        if (typeof total === "number") {
          setTotalCount(total);
          setHasMore(pageParam < Math.ceil(total / sizeParam));
        } else {
          setTotalCount(null);
          setHasMore(items.length === sizeParam);
        }
      } else if (Array.isArray(response)) {
        const items = response;
        setProperties(items);
        setTotalCount(null);
        setHasMore(items.length === sizeParam);
      } else {
        setProperties([]);
        setTotalCount(null);
        setHasMore(false);
      }
    } catch (err) {
      console.log(err);
      setHasMore(false);
    } finally {
      setLoading(false);
    }
  };

  const handleFilterChange = (e) => {
    const { name, value } = e.target;
    setFilters((prev) => ({ ...prev, [name]: value }));
  };

  const handleSearch = () => {
    const nextPage = 1;
    setAppliedFilters(filters);
    setCurrentPage(nextPage);
    fetchProperties(filters, nextPage, pageSize);
  };

  if (loading) {
    return (
      <div className="flex justify-center items-center h-64">
        <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600"></div>
      </div>
    );
  }

  return (
    <div className="space-y-6">
      {successMessage && (
        <div className="bg-green-50 border border-green-200 rounded-md p-4">
          <div className="flex">
            <div className="text-green-800">{successMessage}</div>
            <button
              onClick={() => setSuccessMessage("")}
              className="ml-auto text-green-600 hover:text-green-800"
            >
              ×
            </button>
          </div>
        </div>
      )}

      <div className="bg-white rounded-lg shadow p-6">
        <h2 className="text-lg font-medium text-gray-900 mb-4">
          Search Properties
        </h2>
        <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
          <div>
            <label
              htmlFor="type"
              className="block text-sm font-medium text-gray-700 mb-1"
            >
              Property Type
            </label>
            <select
              id="type"
              name="type"
              value={filters.type}
              onChange={handleFilterChange}
              className="w-full border border-gray-300 rounded-md px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
            >
              <option value="">All Types</option>
              <option value="apartment">Apartment</option>
              <option value="house">House</option>
              <option value="condo">Condo</option>
              <option value="townhouse">Townhouse</option>
            </select>
          </div>

          <div>
            <label
              htmlFor="minPrice"
              className="block text-sm font-medium text-gray-700 mb-1"
            >
              Min Price
            </label>
            <input
              type="number"
              id="minPrice"
              name="minPrice"
              value={filters.minPrice}
              onChange={handleFilterChange}
              placeholder="0"
              className="w-full border border-gray-300 rounded-md px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
          </div>

          <div>
            <label
              htmlFor="maxPrice"
              className="block text-sm font-medium text-gray-700 mb-1"
            >
              Max Price
            </label>
            <input
              type="number"
              id="maxPrice"
              name="maxPrice"
              value={filters.maxPrice}
              onChange={handleFilterChange}
              placeholder="1000000"
              className="w-full border border-gray-300 rounded-md px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
          </div>

          <div className="md:col-span-3 lg:col-span-3 flex justify-end gap-4">
            <button
              type="button"
              onClick={handleSearch}
              className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500"
            >
              Search
            </button>
            <button
              type="button"
              onClick={() => {
                const empty = { type: "", minPrice: "", maxPrice: "" };
                setFilters(empty);
                setAppliedFilters(empty);
                const nextPage = 1;
                setCurrentPage(nextPage);
                fetchProperties(empty, nextPage, pageSize);
              }}
              className="px-4 py-2 border border-gray-300 text-gray-700 rounded-md hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-blue-500"
            >
              Clear Filters
            </button>
          </div>
        </div>
      </div>

      <div className="bg-white rounded-lg shadow">
        <div className="px-6 py-4 border-b border-gray-200">
          <div className="flex items-center justify-between">
            <h3 className="text-lg font-medium text-gray-900">
              Results ({properties.length} properties)
            </h3>
            {(appliedFilters.type || appliedFilters.minPrice || appliedFilters.maxPrice) &&  (
              <div className="text-sm text-gray-500">
                Active filters:
                {appliedFilters.type && (
                  <span className="ml-2 px-2 py-1 bg-blue-100 text-blue-800 rounded">
                    Type: {appliedFilters.type}
                  </span>
                )}
                {appliedFilters.minPrice && (
                  <span className="ml-2 px-2 py-1 bg-blue-100 text-blue-800 rounded">
                    Min: ${parseInt(appliedFilters.minPrice).toLocaleString()}
                  </span>
                )}
                {appliedFilters.maxPrice && (
                  <span className="ml-2 py-1 bg-blue-100 text-blue-800 rounded">
                    Max: ${parseInt(appliedFilters.maxPrice).toLocaleString()}
                  </span>
                )}
              </div>
            )}
          </div>
        </div>

        <div className={` ${properties.length > 0 ? 'block divide-y divide-gray-200 max-h-[700px] overflow-y-auto pr-1' : 'hidden'}`}>
          {properties.map((property) => (
            <div
              key={property.id}
              className="p-6 hover:bg-gray-50 transition-colors"
            >
              <div className="flex items-start space-x-4">
                <div className="flex-1 min-w-0">
                  <div className="flex items-center justify-between">
                    <h4 className="text-lg font-medium text-gray-900 truncate">
                      {property.address}
                    </h4>
                    <span className="text-lg font-semibold text-blue-600">
                      ${property.price.toLocaleString()}
                    </span>
                  </div>

                  <p className="text-sm text-gray-600 mt-1">
                    {property.description}
                  </p>
                  <p className="text-sm text-gray-500 mt-1">
                    {property.numberOfSpaces} spaces | Avg : {property.spaceAverage.toFixed(2)} sq ft
                  </p>

                  <div className="flex items-center mt-3 space-x-4">
                    <span className="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium bg-blue-100 text-blue-800">
                      {property.type}
                    </span>
                    <Link
                      to={`/property/${property.id}`}
                      className="text-blue-600 hover:text-blue-800 text-sm font-medium"
                    >
                      View Details →
                    </Link>
                  </div>
                </div>
              </div>
            </div>
          ))}
        </div>

        {properties.length === 0 && (
          <div className="p-6 text-gray-500 h-[500px] flex items-center justify-center">
            No properties found matching your criteria.
          </div>
        )}

        <div className="px-6 py-4 border-t border-gray-200 flex items-center justify-between">
          <div className="flex items-center gap-2 text-sm text-gray-600">
            <span>Rows per page:</span>
            <select
              className="border border-gray-300 rounded-md px-2 py-1"
              value={pageSize}
              onChange={(e) => {
                const size = parseInt(e.target.value) || 10;
                const nextPage = 1;
                setPageSize(size);
                setCurrentPage(nextPage);
                fetchProperties(appliedFilters, nextPage, size);
              }}
            >
              <option value={5}>5</option>
              <option value={10}>10</option>
              <option value={20}>20</option>
              <option value={50}>50</option>
            </select>
          </div>
          <div className="flex items-center gap-2">
            <button
              type="button"
              className="px-3 py-1 border rounded disabled:opacity-50"
              onClick={() => {
                if (currentPage <= 1) return;
                const prev = currentPage - 1;
                setCurrentPage(prev);
                fetchProperties(appliedFilters, prev, pageSize);
              }}
              disabled={currentPage <= 1}
            >
              Previous
            </button>
            <span className="text-sm text-gray-600">
              Page {currentPage}
              {totalCount ? ` of ${Math.max(1, Math.ceil(totalCount / pageSize))}` : ""}
            </span>
            <button
              type="button"
              className="px-3 py-1 border rounded disabled:opacity-50"
              onClick={() => {
                const allowed = totalCount != null
                  ? currentPage < Math.ceil(totalCount / pageSize)
                  : hasMore === true;
                if (!allowed) return;
                const next = currentPage + 1;
                setCurrentPage(next);
                fetchProperties(appliedFilters, next, pageSize);
              }}
              disabled={totalCount != null ? currentPage >= Math.ceil(totalCount / pageSize) : hasMore === false}
            >
              Next
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};


export default SearchPage;
