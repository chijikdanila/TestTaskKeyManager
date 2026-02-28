using BLL.Services;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Entities.Entities;
using Primitives.Enums;

namespace Services.Services;

public class DocxGenerator : IOrganizationsDataReportService
{
    public void SaveOrganizationsReport(List<Organization> organizations, string filePath)
    {
        if (File.Exists(filePath))
        {
            throw new Exception($"Файл с названием {filePath} уже существует");
        };
        
        using (var doc = WordprocessingDocument.Create($"{filePath}.docx", WordprocessingDocumentType.Document))
        {
            var mainPart = doc.AddMainDocumentPart();
            mainPart.Document = new Document(new Body());
            var body = mainPart.Document.Body!;

            foreach (var org in organizations)
            {
                var title = new Paragraph(
                    new ParagraphProperties(new SpacingBetweenLines { Before = "240", After = "120" }),
                    new Run(new RunProperties(new Bold(), new FontSize { Val = "32" }), 
                    new Text($"Организация: {org.Name}"))
                );
                body.AppendChild(title);

                var table = CreateStyledTable();

                table.AppendChild(CreateRow(true, "ID", "Key Value", "Started", "Ended", "Status"));

                if (org.KeysInfo is not null && org.KeysInfo.Any())
                {
                    foreach (var key in org.KeysInfo)
                    {
                        table.AppendChild(CreateRow(false,
                            key.Id.ToString(),
                            key.KeyValue.ToString(),
                            key.StartedAt.ToShortDateString(),
                            key.EndedAt.ToShortDateString(),
                            key.KeyStatus.ToMessage()
                        ));
                    }
                }
                else
                {
                    table.AppendChild(CreateRow(false, "Нет данных о ключах", "", "", "", ""));
                }

                body.AppendChild(table);
                body.AppendChild(new Paragraph(new Run(new Break())));
            }

            mainPart.Document.Save();
        }
    }

    private static Table CreateStyledTable()
    {
        var table = new Table();
        var props = new TableProperties(
            new TableBorders(
                new TopBorder { Val = BorderValues.Single, Size = 4 },
                new BottomBorder { Val = BorderValues.Single, Size = 4 },
                new LeftBorder { Val = BorderValues.Single, Size = 4 },
                new RightBorder { Val = BorderValues.Single, Size = 4 },
                new InsideHorizontalBorder { Val = BorderValues.Single, Size = 4 },
                new InsideVerticalBorder { Val = BorderValues.Single, Size = 4 }
            ),
            
            new TableWidth { Width = "6200", Type = TableWidthUnitValues.Pct },
            new TableLayout { Type = TableLayoutValues.Fixed }
        );
        table.AppendChild(props);
        
        var grid = new TableGrid(
            new GridColumn { Width = "500" },
            new GridColumn { Width = "2500" },
            new GridColumn { Width = "1000" },
            new GridColumn { Width = "1000" },
            new GridColumn { Width = "1200" }
        );
        table.AppendChild(grid);

        return table;
    }

    private static TableRow CreateRow(bool isHeader, params string[] texts)
    {
        var row = new TableRow();
        
        foreach (var text in texts)
        {
            var runProps = new RunProperties(new FontSize { Val = "18" });
            if (isHeader) runProps.Append(new Bold());

            var cellProps = new TableCellProperties(
                new NoWrap() 
            );

            var cell = new TableCell();
            cell.Append(cellProps);
            cell.Append(new Paragraph(new Run(runProps, new Text(text))));
        
            row.Append(cell);
        }
        
        return row;
    }
}