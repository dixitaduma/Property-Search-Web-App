import axios from 'axios'

const API_URL = import.meta.env.VITE_API_URL 

export const getPropertyById = async (id) => {
  const response = await axios.get(`${API_URL}Properties/GetPropertyById?id=${id}`)
  return response.data
}

export const createProperty = async (propertyData) => {
  const response = await axios.post(`${API_URL}Properties/AddProperty`, propertyData)
  return response.data
}   

export const getPropertyWithFilters = async (filters) => {
  const response = await axios.get(`${API_URL}Properties/GetProperties`, { params: filters })
  return response.data.data
}

export const getAllSpaces = async (id, pageNumber, pageSize) => {
  const response = await axios.get(`${API_URL}Spaces/GetAllSpaces`, {
    params: { propertyId: id, pageNumber, pageSize }
  })
  return response.data.data
}