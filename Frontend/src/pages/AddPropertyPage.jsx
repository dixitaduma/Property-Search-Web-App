import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { Formik, Form, Field, ErrorMessage } from 'formik'
import * as Yup from 'yup'
import { createProperty } from '../services/api'

const validationSchema = Yup.object({
  type: Yup.string()
    .oneOf(['apartment', 'house', 'condo', 'townhouse'], 'Please select a valid property type')
    .required('Property type is required'),
  price: Yup.number()
    .positive('Price must be a positive number')
    .min(1000, 'Price must be at least $1,000')
    .max(100000000, 'Price must be less than $100,000,000')
    .required('Price is required'),
  address: Yup.string()
    .min(10, 'Address must be at least 10 characters')
    .max(200, 'Address must be less than 200 characters')
    .required('Address is required'),
  description: Yup.string()
    .max(1000, 'Description must be less than 1000 characters'),
  spaces: Yup.array().of(
    Yup.object({
      type: Yup.string()
        .oneOf(['bedroom', 'bathroom', 'kitchen', 'living room', 'dining room', 'office', 'garage', 'basement', 'attic', 'other'], 'Please select a valid space type')
        .required('Space type is required'),
      size: Yup.number()
        .positive('Size must be a positive number')
        .min(10, 'Size must be at least 10 sq ft')
        .max(5000, 'Size must be less than 5,000 sq ft')
        .required('Space size is required'),
      description: Yup.string()
        .max(200, 'Description must be less than 200 characters')
    })
  )
})

const AddPropertyPage = () => {
  const navigate = useNavigate()

  const initialValues = {
    type: '',
    price: '',
    address: '',
    description: '',
    spaces: []
  }

  const handleSubmit = async (values, formikHelpers) => {
    
    const processedData = {
      ...values,
      price: parseFloat(values.price),
      spaces: (values.spaces || []).map((space, index) => ({
        id: Date.now() + index,
        property_id: null, 
        type: space.type,
        size: parseInt(space.size),
        description: space.description || ''
      }))
    }
    
    console.log('Payload', processedData)
    const response = await createProperty(processedData)
    console.log('Response', response)
    navigate('/', { state: { message: 'Property created successfully!' } })
    if (formikHelpers && formikHelpers.setSubmitting) {
      formikHelpers.setSubmitting(false)
    }
  }

    return (
    <div className="max-w-2xl mx-auto">
      <div className="flex items-center justify-between mb-6">
        <h1 className="text-2xl sm:text-3xl font-bold text-gray-900">Add New Property</h1>
        <button
          onClick={() => navigate('/')}
          className="text-blue-600 hover:text-blue-800 flex items-center"
        >
          <span className="text-2xl sm:text-base">←</span>
          <span className="hidden sm:inline ml-1">Back to Search</span>
        </button>
             </div>

      <div className="bg-white rounded-lg shadow p-6">
        <Formik
          initialValues={initialValues}
          validationSchema={validationSchema}
          onSubmit={handleSubmit}
          validateOnChange={true}
          validateOnBlur={true}
        >
          {({ isSubmitting, errors, touched, status, values, setFieldValue, isValid, dirty }) => (
            <Form className="space-y-6">
              <div className="grid grid-cols-1 sm:grid-cols-2 gap-6">
                <div>
                  <label htmlFor="type" className="block text-sm font-medium text-gray-700 mb-1">
                    Property Type *
                  </label>
                  <Field
                    as="select"
                    id="type"
                    name="type"
                    className={`w-full border rounded-md px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500 ${
                      errors.type && touched.type ? 'border-red-300' : 'border-gray-300'
                    }`}
                  >
                    <option value="">Select Type</option>
                    <option value="apartment">Apartment</option>
                    <option value="house">House</option>
                    <option value="condo">Condo</option>
                    <option value="townhouse">Townhouse</option>
                  </Field>
                  <ErrorMessage name="type" component="div" className="text-red-600 text-sm mt-1" />
                </div>

                <div>
                  <label htmlFor="price" className="block text-sm font-medium text-gray-700 mb-1">
                    Price *
                  </label>
                  <Field
                    type="number"
                    id="price"
                    name="price"
                    className={`w-full border rounded-md px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500 ${
                      errors.price && touched.price ? 'border-red-300' : 'border-gray-300'
                    }`}
                    placeholder="450000"
                  />
                  <ErrorMessage name="price" component="div" className="text-red-600 text-sm mt-1" />
                </div>

                <div className="sm:col-span-2">
                  <label htmlFor="address" className="block text-sm font-medium text-gray-700 mb-1">
                    Address *
                  </label>
                  <Field
                    type="text"
                    id="address"
                    name="address"
                    className={`w-full border rounded-md px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500 ${
                      errors.address && touched.address ? 'border-red-300' : 'border-gray-300'
                    }`}
                    placeholder="e.g., 123 Main St, Downtown, City"
                  />
                  <ErrorMessage name="address" component="div" className="text-red-600 text-sm mt-1" />
                </div>
              </div>

              <div>
                <label htmlFor="description" className="block text-sm font-medium text-gray-700 mb-1">
                  Description
                </label>
                <Field
                  as="textarea"
                  id="description"
                  name="description"
                  rows="4"
                  className={`w-full border rounded-md px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500 ${
                    errors.description && touched.description ? 'border-red-300' : 'border-gray-300'
                  }`}
                  placeholder="Describe the property..."
                />
                <ErrorMessage name="description" component="div" className="text-red-600 text-sm mt-1" />
              </div>


              <div className="border-t pt-6">
                <div className="flex items-center justify-between mb-4">
                  <h3 className="text-lg font-medium text-gray-900">Property Spaces</h3>
                  <button
                    type="button"
                    onClick={() => {
                      const currentSpaces = values.spaces || []
                      setFieldValue('spaces', [...currentSpaces, { type: '', size: '', description: '' }])
                    }}
                    className="bg-green-600 hover:bg-green-700 text-white px-3 py-1 rounded-md text-sm font-medium"
                  >
                    + Add Space
                  </button>
                </div>
                
                <div className="space-y-4 max-h-[400px] overflow-y-auto">
                  {values.spaces && values.spaces.map((space, index) => (
                    <div key={index} className="border border-gray-200 rounded-lg p-4 bg-gray-50">
                      <div className="flex items-center justify-between mb-3">
                        <h4 className="text-md font-medium text-gray-800">Space {index + 1}</h4>
                        <button
                          type="button"
                          onClick={() => {
                            const newSpaces = values.spaces.filter((_, i) => i !== index)
                            setFieldValue('spaces', newSpaces)
                          }}
                          className="text-red-600 hover:text-red-800 text-sm"
                        >
                          Remove
                        </button>
                      </div>
                      
                      <div className="grid grid-cols-1 gap-4">
                        <div>
                          <label className="block text-sm font-medium text-gray-700 mb-1">
                            Space Type *
                          </label>
                          <Field
                            as="select"
                            name={`spaces.${index}.type`}
                            className={`w-full border rounded-md px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500 ${
                              errors.spaces?.[index]?.type && touched.spaces?.[index]?.type ? 'border-red-300' : 'border-gray-300'
                            }`}
                          >
                            <option value="">Select Type</option>
                            <option value="bedroom">Bedroom</option>
                            <option value="bathroom">Bathroom</option>
                            <option value="kitchen">Kitchen</option>
                            <option value="living room">Living Room</option>
                            <option value="dining room">Dining Room</option>
                            <option value="office">Office</option>
                            <option value="garage">Garage</option>
                            <option value="basement">Basement</option>
                            <option value="attic">Attic</option>
                            <option value="other">Other</option>
                          </Field>
                          <ErrorMessage name={`spaces.${index}.type`} component="div" className="text-red-600 text-sm mt-1" />
                        </div>
                        
                        <div>
                          <label className="block text-sm font-medium text-gray-700 mb-1">
                            Size (sq ft) *
                          </label>
                          <Field
                            type="number"
                            name={`spaces.${index}.size`}
                            className={`w-full border rounded-md px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500 ${
                              errors.spaces?.[index]?.size && touched.spaces?.[index]?.size ? 'border-red-300' : 'border-gray-300'
                            }`}
                            placeholder="150"
                          />
                          <ErrorMessage name={`spaces.${index}.size`} component="div" className="text-red-600 text-sm mt-1" />
                        </div>
                        
                        <div>
                          <label className="block text-sm font-medium text-gray-700 mb-1">
                            Description
                          </label>
                          <Field
                            type="text"
                            name={`spaces.${index}.description`}
                            className={`w-full border rounded-md px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500 ${
                              errors.spaces?.[index]?.description && touched.spaces?.[index]?.description ? 'border-red-300' : 'border-gray-300'
                            }`}
                            placeholder="Optional description"
                          />
                          <ErrorMessage name={`spaces.${index}.description`} component="div" className="text-red-600 text-sm mt-1" />
                        </div>
                      </div>
                    </div>
                  ))}
                  
                  {(!values.spaces || values.spaces.length === 0) && (
                    <div className="text-center py-8 text-gray-500">
                      No spaces added yet. Click "Add Space" to create property spaces.
                    </div>
                  )}
                </div>
              </div>

              {status && (
                <div className="bg-red-50 border border-red-200 rounded-md p-4">
                  <div className="text-red-800">{status}</div>
                </div>
              )}

              <div className="flex flex-col sm:flex-row sm:justify-end gap-3">
                <button
                  type="button"
                  onClick={() => navigate('/')}
                  className="w-full sm:w-auto px-4 py-2.5 border border-gray-300 rounded-md text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-blue-500 font-medium"
                >
                  Cancel
                </button>
                <button
                  type="submit"
                  className="w-full sm:w-auto px-6 py-2.5 bg-blue-600 hover:bg-blue-700 disabled:bg-blue-400 text-white font-medium rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 transition-colors"
                >
                  {isSubmitting ? 'Creating...' : 'Create Property'}
                </button>
              </div>
            </Form>
          )}
        </Formik>
      </div>
    </div>
  )
}

export default AddPropertyPage
