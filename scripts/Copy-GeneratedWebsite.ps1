Write-Host "Copying a solution with generated website to a separate folder."
Write-Host "Warning: Existing files in the folder will be overwritten!"
$GeneratedWebsiteFolder = Read-Host -Prompt "Enter a folder name for your static website"

Copy-Item "..\PDFiller.Models" -Destination "..\GeneratedWebsites\$GeneratedWebsiteFolder\PDFiller.Models" -Recurse -Force
Write-Host "Successfully copied the PDFiller.Models project."

Copy-Item "..\PDFiller.Website" -Destination "..\GeneratedWebsites\$GeneratedWebsiteFolder\PDFiller.Website" -Recurse -Force
Write-Host "Successfully copied the PDFiller.Website project."

Copy-Item "..\PDFiller.Website.sln" -Destination "..\GeneratedWebsites\$GeneratedWebsiteFolder\PDFiller.Website.sln" -Force
Write-Host "Successfully copied the solution file."

Copy-Item "..\LICENSE" -Destination "..\GeneratedWebsites\$GeneratedWebsiteFolder\LICENSE" -Force
Write-Host "Successfully copied the license file."

Write-Host "Generated website is available in ""..\GeneratedWebsites\$GeneratedWebsiteFolder""."

