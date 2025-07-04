GS1 Label Generator
A dynamic web application for building GS1-compliant product labels with customizable fields and live validation.

Features
Dynamically add/remove GS1 fields such as GTIN, batch number, production date, expiration date, serial number, and more.

Field-specific input validation, including GTIN checksum validation and date format checking (YYMMDD).

Preset templates for common label types (Pharma, Food, Logistics) that pre-populate relevant GS1 fields.

Dark mode toggle for better user experience in low-light environments.

Tooltips for guidance on each GS1 field.

Responsive and clean UI built with Bootstrap 5.

Technologies Used
ASP.NET MVC for server-side rendering and model binding.

Bootstrap 5 for responsive design and UI components.

Vanilla JavaScript for dynamic field management, validation, and dark mode toggle.

HTML5 & CSS3 for markup and styling.

Getting Started
Prerequisites
.NET SDK (for running ASP.NET MVC projects)

A web browser (Chrome, Firefox, Edge, etc.)

Running the Project
Clone the repository:

bash
Copy
Edit
git clone https://github.com/yourusername/gs1-label-generator.git
Open the project in your preferred IDE (e.g., Visual Studio).

Restore dependencies and build the solution.

Run the application locally.

Access the application in your browser at http://localhost:{port}.

Usage
Use the Add Field button to add a GS1 data field to your label.

Select the field type from the dropdown.

Enter the corresponding value; input validation will guide you.

Use the Remove button to delete a field.

Select a preset label template to auto-populate fields relevant to that industry.

Toggle between light and dark mode using the button in the top-right corner.

Submit your completed label form for backend processing.

Validation Details
GTIN: Must be exactly 14 digits with a valid GS1 checksum.

Dates (Production, Best Before, Expiration): Must follow YYMMDD format and represent a valid calendar date.

Other fields have custom validation rules for length, format, and numeric ranges.

Future Improvements
Server-side validation to complement client-side checks.

Barcode generation and preview.

Export labels in PDF or image format.

User authentication for saving and managing label templates.

Enhanced accessibility features.
