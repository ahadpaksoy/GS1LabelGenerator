# GS1 Label Generator

A dynamic, preset-driven web application for building and previewing GS1-compliant product labels. This tool streamlines the creation of complex GS1 strings by using configurable templates, providing live previews, and exporting the final label as a PDF.

<table>
  <tr>
    <td align="center"><strong>Light Mode</strong></td>
    <td align="center"><strong>Dark Mode</strong></td>
  </tr>
  <tr>
    <td>
      <img src="https://i.imgur.com/JuBF88p.png" alt="GS1 Label Builder in Light Mode" width="100%">
    </td>
    <td>
      <img src="https://i.imgur.com/bm4ac1d.png" alt="GS1 Label Builder in Dark Mode" width="100%">
    </td>
  </tr>
</table>

---

## ✨ Features

- **Preset-Driven UI**: The form is built dynamically based on pre-configured templates. The manual addition and removal of fields has been removed to ensure consistency and ease of use.
- **Live Preview**: See the generated GS1 string and a scannable QR code update in real-time as you type.
- **Configurable Presets**:
    - **Built-in Templates**: Comes with default presets for common label types (e.g., Pharma, Food, Logistics) loaded directly from `presets.json`.
    - **Dynamic Preset Upload**: Users can upload their own `presets.json` file to add custom templates to the dropdown menu on the fly.
- **Real-Time Input Validation**: Client-side validation provides immediate feedback on data formatting, including:
    - GTIN check-digit calculation
    - Correct date formatting (YYMMDD)
    - Field-specific length and character-type rules
- **PDF Export**: Generate and download a professional A6-sized PDF of your final label, complete with the GS1 data, a Code-128 barcode, and a QR code.
- **Modern UI/UX**:
    - Clean and responsive interface built with Bootstrap 5.
    - Dark mode toggle for a comfortable experience in any lighting.
    - Informative tooltips for each GS1 field to guide data entry.

---

## 🛠️ Technologies Used

- **Backend**: ASP.NET MVC, C#
- **Frontend**: HTML5, CSS3, Bootstrap 5, Vanilla JavaScript
- **Barcode/QR Generation**: ZXing.Net (Barcode), QRCoder (QR Code)
- **PDF Generation**: iTextSharp
- **Data Serialization**: Newtonsoft.Json

---

## 🚀 Getting Started

### Prerequisites

- [.NET Framework 4.7.2 or later](https://dotnet.microsoft.com/download/dotnet-framework)
- [Visual Studio](https://visualstudio.microsoft.com/vs/) with the ASP.NET and web development workload installed.

### Running the Project

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/ahadpaksoy/GS1LabelGenerator.git
    cd GS1LabelGenerator
    ```

2.  **Open the solution in Visual Studio:**
    - Double-click the `.sln` file to open the project.

3.  **Run the application:**
    - Press `F5` or click the "Start Debugging" button (usually a green play icon).
    - Visual Studio will build the project and open it in your default web browser.

---

## ⚙️ Configuration

The application is configured using a simple JSON file located at the root of the project.

### `presets.json`

This file defines the templates that appear in the dropdown menu. The key is the display name of the preset, and the value is an array of strings corresponding to the property names in `ProductLabelModel.cs`.

**Example `presets.json`:**
```json
{
  "💊 Pharma Label": [
    "GTIN",
    "batch_lotNumber",
    "expirationDate",
    "serialNumber"
  ],
  "🚛 Logistics Label": [
    "sscc",
    "GTIN",
    "countContained",
    "customerPONumber"
  ]
}
