import React from 'react'
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom'
import SearchPage from './pages/SearchPage'
import DetailPage from './pages/DetailPage'
import AddPropertyPage from './pages/AddPropertyPage'

function App() {
  return (
    <Router>
      <div className="min-h-screen bg-gray-50">
        <nav className="bg-white shadow-sm border-b fixed top-0 left-0 right-0 z-50">
          <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            <div className="flex justify-between h-16">
              <div className="flex items-center">
                <Link to="/">
                  <h1 className="text-xl font-semibold text-gray-900">Property Search</h1>
                </Link>
              </div>
              <div className="flex items-center space-x-4">
                <Link to="/" className="text-gray-600 hover:text-gray-900 px-3 py-2 rounded-md text-sm font-medium">Search</Link>
                <Link to="/add" className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-md text-sm font-medium">Add Property</Link>
              </div>
            </div>
          </div>
        </nav>
        
                    <main className="max-w-7xl mx-auto py-4 sm:py-6 px-4 sm:px-6 lg:px-8 mt-16">
          <Routes>
            <Route path="/" element={<SearchPage />} />
            <Route path="/property/:id" element={<DetailPage />} />
            <Route path="/add" element={<AddPropertyPage />} />
          </Routes>
        </main>
      </div>
    </Router>
  )
}

export default App
