# GS1 Label Generator — README

> A dynamic, preset-driven ASP.NET MVC app for building GS1-compliant labels with live preview (string + QR), Code-128 barcode, and PDF export. 
## Table of contents

* [Overview](#overview) • [Features](#features) • [Tech stack](#tech-stack)
* [Quickstart](#quickstart) • [Configuration](#configuration) • [Usage](#usage)
* [Architecture](#architecture) • [Troubleshooting](#troubleshooting) • [Roadmap](#roadmap) • [License](#license)

---

## Overview

Purchasing/operations teams often need to generate GS1 labels quickly and consistently across product lines. This tool provides a **template-based form** that renders the correct GS1 string as you type, shows a **scannable QR code**, produces a **Code-128 barcode**, and exports an **A6 PDF** ready for printing.

  <tr>
    <td align="center"><strong>Input Page</strong></td>
    <td align="center"><strong>Submit Page</strong></td>
  </tr>
  <tr>
    <td>
      <img src="https://i.imgur.com/JuBF88p.png" alt="GS1 Label Builder Input Page" width="100%">
    </td>
    <td>
      <img src="https://i.imgur.com/bm4ac1d.png" alt="GS1 Label Builder in Submit Page" width="100%">
    </td>
  </tr>
</table>

---

## Features

* **Preset-driven UI:** The form is built dynamically from templates; manual add/remove of fields is removed for consistency. 
* **Live preview:** Instant GS1 string + QR preview as you type. 
* **Configurable presets:**

  * Built-in templates (e.g., Pharma, Food, Logistics) from `presets.json`.
  * **Upload your own `presets.json`** to add more templates on the fly.
* **Real-time validation:** GTIN check digit, YYMMDD date format, and field-specific length/character rules. 
* **PDF export:** Generates a professional A6 PDF, including the GS1 data, **Code-128** barcode, and a QR code. 
* **Modern UI/UX:** Bootstrap 5, responsive layout, dark-mode toggle, and tooltips for each GS1 field.

---

## Tech stack

* **Backend:** ASP.NET MVC (C#)
* **Frontend:** HTML5, CSS3, Bootstrap 5, Vanilla JS
* **Barcode/QR:** **ZXing.Net** for barcodes, **QRCoder** for QR codes
* **PDF:** **iTextSharp**
* **JSON:** Newtonsoft.Json 

> ZXing.Net is a .NET port of ZXing for multi-format barcodes (incl. Code-128). QRCoder is a pure C# library for QR codes. iTextSharp is a .NET PDF library.

---

## Quickstart

### Prerequisites

* **.NET Framework 4.7.2 or later**
* **Visual Studio** with the *ASP.NET and web development* workload installed. 

### Run locally

```bash
# 1) Clone
git clone https://github.com/ahadpaksoy/GS1LabelGenerator.git
cd GS1LabelGenerator

# 2) Open solution
#   Double-click the .sln in Visual Studio

# 3) Start
#   Press F5 (Start Debugging) to build & launch in your browser
```

Steps above match the repo’s Getting Started guidance.

---

## Configuration

### `presets.json`

Templates shown in the UI are defined in a JSON file at the repo root.

* **Key:** The display name in the preset dropdown
* **Value:** An array of **property names from `ProductLabelModel.cs`** (the server-side data model) which the form will render in order. 

**Example**

```json
{
  "Pharma Label": ["GTIN", "batch_lotNumber", "expirationDate", "serialNumber"],
  "Logistics Label": ["sscc", "GTIN", "countContained", "customerPONumber"]
}
```

This example mirrors the one from the repo README. ([GitHub][1])

> FYI (GS1 AIs commonly used): **SSCC (00)**, **GTIN (01)**, **Batch/Lot (10)**, **Expiry (17)**, **Serial (21)** — useful when designing your templates. 

---

## Usage

1. **Pick a preset** (e.g., Pharma/Logistics)
2. **Fill required fields** — validation will guide you (check digit for GTIN, dates as YYMMDD, etc.)
3. **Watch live preview** — GS1 string and QR update as you type
4. **Export PDF** — download an **A6** label with Code-128 + QR embedded

> You can also **upload a custom `presets.json`** at runtime to inject new templates into the dropdown without rebuilding. 

---

## Architecture

```mermaid
flowchart LR
  U[User] --> UI[Preset-driven Form (Bootstrap 5)]
  UI --> V[Client-side Validation\n(GTIN check, YYMMDD, lengths)]
  UI --> P[Preview: GS1 string + QR\n(QRCoder)]
  UI -->|Export| S[Server]
  S --> B[Barcode (ZXing.Net)\nCode-128]
  S --> Q[QR (QRCoder)]
  S --> D[PDF (iTextSharp)\nA6 export]
```

* **Barcode/QR generation:** ZXing.Net (barcodes like Code-128) and QRCoder (QR) are used for rendering; both are well-known NuGet packages. 
* **PDF rendering:** iTextSharp assembles the final **A6** label layout for download. 
* **Presets:** `presets.json` maps directly to model properties, so the form can be rebuilt dynamically from templates. 

---

## Troubleshooting

* **Build succeeds but PDF export fails** → ensure iTextSharp is referenced and compatible with your target framework. Note that **iTextSharp 5 is EOL**; for production you may consider migrating to **iText 9** (different API/licensing). 
* **Fields missing in the form** → check that the **property names in `presets.json`** match the model exactly. 
* **Invalid GTIN or dates** → use proper **check digit** and **YYMMDD** format; the app validates and will show hints. 

---
[5]: https://itextpdf.com/products/itextsharp?utm_source=chatgpt.com "iTextSharp | iText PDF"
[6]: https://www.nuget.org/packages/itextsharp/?utm_source=chatgpt.com "iTextSharp 5.5.13.4"
