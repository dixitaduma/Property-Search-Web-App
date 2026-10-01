import React from 'react'
import { useState, useEffect } from 'react'
import { useParams, Link } from 'react-router-dom'
import { getAllSpaces, getPropertyById } from '../services/api'

const DetailPage = () => {
  const { id } = useParams()
  const [property, setProperty] = useState(null)
  const [loading, setLoading] = useState(true)
  const [spaceFilter, setSpaceFilter] = useState('')
  const [error, setError] = useState(null)
  const [spaces, setSpaces] = useState([])
  const [spacesPage, setSpacesPage] = useState(1)
  const [spacesPageSize, setSpacesPageSize] = useState(5)
  const [spacesTotal, setSpacesTotal] = useState(null)
  const [spacesHasMore, setSpacesHasMore] = useState(null)

  useEffect(() => {
    fetchPropertyAndSpaces(spacesPage, spacesPageSize)
  }, [id, spacesPage, spacesPageSize])

  const fetchPropertyAndSpaces = async (pageOverride, sizeOverride) => {
    setLoading(true)
    setError(null)
    try {
      const page = pageOverride ?? spacesPage
      const size = sizeOverride ?? spacesPageSize
      const [prop, spacesResp] = await Promise.all([
        getPropertyById(id),
        getAllSpaces(id, page, size)
      ])
      const propertyData = prop && prop.data ? prop.data : prop
      const items = Array.isArray(spacesResp?.data)
        ? spacesResp.data
        : (Array.isArray(spacesResp?.spaces)
          ? spacesResp.spaces
          : (Array.isArray(spacesResp) ? spacesResp : []))
      const total = spacesResp?.totalCount ?? spacesResp?.total ?? spacesResp?.count ?? spacesResp?.totalRecords ?? null
      setProperty(propertyData)
      setSpaces(items)
      if (typeof total === 'number') {
        setSpacesTotal(total)
        setSpacesHasMore(page < Math.ceil(total / size))
      } else {
        setSpacesTotal(null)
        setSpacesHasMore(items.length === size)
      }
    } catch (err) {
      setSpacesTotal(null)
      setSpacesHasMore(false)
    } finally {
      setLoading(false)
    }
  }

  if (loading) {
    return (
      <div className="flex justify-center items-center h-64">
        <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600"></div>
      </div>
    )
  }

  if (!property) {
    return (
      <div className="bg-red-50 border border-red-200 rounded-md p-4">
        <div className="text-red-800">Property not found</div>
        <Link to="/" className="text-blue-600 hover:text-blue-800 mt-2 inline-block">
          ← Back to Search
        </Link>
      </div>
    )
  }

  const filteredSpaces = spaces.filter(space => 
    space.type.toLowerCase().includes(spaceFilter.toLowerCase())
  )

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <Link to="/" className="text-blue-600 hover:text-blue-800 flex items-center">
          <span className="text-2xl sm:text-base">←</span>
          <span className="hidden sm:inline ml-1">Back to Search</span>
        </Link>
        <h1 className="text-2xl sm:text-3xl font-bold text-gray-900">{property.address}</h1>
      </div>

      <div className="bg-white rounded-lg shadow overflow-hidden">
        <div className="p-6">
          <div className="space-y-6">
            <div className="flex items-center justify-between">
              <span className="text-2xl font-bold text-blue-600">${property.price.toLocaleString()}</span>
              <span className="inline-flex items-center px-3 py-1 rounded-full text-sm font-medium bg-blue-100 text-blue-800">
                {property.type.charAt(0).toUpperCase() + property.type.slice(1)}
              </span>
            </div>

            <div className="grid grid-cols-2 gap-4 text-center">
              <div>
                <div className="text-2xl font-semibold text-gray-900">{spaces.length}</div>
                <div className="text-sm text-gray-600">Total Spaces</div>
              </div>
              <div>
                <div className="text-2xl font-semibold text-gray-900">{spaces.reduce((total, space) => total + space.size, 0)}</div>
                <div className="text-sm text-gray-600">Total Sq Ft</div>
              </div>
            </div>

            <div className="grid grid-cols-1 md:grid-cols-2 gap-6 pt-4 border-t border-gray-200">
              <div>
                <h3 className="text-lg font-medium text-gray-900 mb-2">Address</h3>
                <p className="text-gray-600">{property.address}</p>
              </div>

              <div>
                <h3 className="text-lg font-medium text-gray-900 mb-2">Description</h3>
                <p className="text-gray-900">{property.description}</p>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div className="bg-white rounded-lg shadow">
        <div className="px-6 py-4 border-b border-gray-200">
          <div className="flex items-center justify-between">
            <h3 className="text-lg font-medium text-gray-900">Spaces ({filteredSpaces.length})</h3>
            <div className="flex items-center space-x-4">
              <input
                type="text"
                placeholder="Filter spaces..."
                value={spaceFilter}
                onChange={(e) => setSpaceFilter(e.target.value)}
                className="border border-gray-300 rounded-md px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
              />
            </div>
          </div>
        </div>

        <div className={filteredSpaces.length > 0 ? 'divide-y divide-gray-200 max-h-[700px] overflow-y-auto pr-1' : 'hidden'}>
          {filteredSpaces.map(space => (
            <div key={space.id} className="p-6 hover:bg-gray-50 transition-colors">
              <div className="flex items-start justify-between">
                <div className="flex-1">
                  <h4 className="text-lg font-medium text-gray-900 capitalize">{space.type}</h4>
                  <p className="text-sm text-gray-600 mt-1">{space.description}</p>
                  <div className="flex items-center mt-2 space-x-4">
                    <span className="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium bg-green-100 text-green-800">
                      {space.type}
                    </span>
                    <span className="text-sm text-gray-500">{space.size} sq ft</span>
                  </div>
                </div>
              </div>
            </div>
          ))}
        </div>

        {filteredSpaces.length === 0 && (
          <div className="p-6 text-gray-500 h-72 flex items-center justify-center">
            No spaces found matching your filter.
          </div>
        )}

        <div className="px-6 py-4 border-t border-gray-200 flex items-center justify-between">
          <div className="flex items-center gap-2 text-sm text-gray-600">
            <span>Rows per page:</span>
            <select
              className="border border-gray-300 rounded-md px-2 py-1"
              value={spacesPageSize}
              onChange={(e) => {
                const size = parseInt(e.target.value) || 5
                setSpacesPage(1)
                setSpacesPageSize(size)
                fetchPropertyAndSpaces(1, size)
              }}
            >
              <option value={5}>5</option>
              <option value={10}>10</option>
              <option value={20}>20</option>
            </select>
          </div>
          <div className="flex items-center gap-2">
            <button
              type="button"
              className="px-3 py-1 border rounded disabled:opacity-50"
              onClick={() => {
                if (spacesPage <= 1) return
                const prev = spacesPage - 1
                setSpacesPage(prev)
                fetchPropertyAndSpaces(prev, spacesPageSize)
              }}
              disabled={spacesPage <= 1}
            >
              Previous
            </button>
            <span className="text-sm text-gray-600">
              Page {spacesPage}
              {spacesTotal ? ` of ${Math.max(1, Math.ceil(spacesTotal / spacesPageSize))}` : ''}
            </span>
            <button
              type="button"
              className="px-3 py-1 border rounded disabled:opacity-50"
              onClick={() => {
                const allowed = spacesTotal != null
                  ? spacesPage < Math.ceil(spacesTotal / spacesPageSize)
                  : spacesHasMore === true
                if (!allowed) return
                const next = spacesPage + 1
                setSpacesPage(next)
                fetchPropertyAndSpaces(next, spacesPageSize)
              }}
              disabled={spacesTotal != null ? spacesPage >= Math.ceil(spacesTotal / spacesPageSize) : spacesHasMore === false}
            >
              Next
            </button>
          </div>
        </div>
      </div>
    </div>
  )
}

const mockProperty = {
  id: 1,
  address: "123 Main St, City, State",
  type: "house",
  price: 300000,
  description: "A beautiful family house",
  spaces: [
    {
      id: 1,
      property_id: 1,
      type: "bedroom",
      size: 150,
      description: "Master bedroom"
    },
    {
      id: 2,
      property_id: 1,
      type: "kitchen",
      size: 100,
      description: "Modern kitchen"
    },
    {
      id: 3,
      property_id: 1,
      type: "living room",
      size: 200,
      description: "Spacious living room"
    }
  ]
}

export default DetailPage
