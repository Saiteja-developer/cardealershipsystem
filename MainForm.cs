using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class MainForm : Form
{
    private Dealership dealership;

    public MainForm()
    {
        dealership = new Dealership();
        CreateMainUI();
    }

    private void CreateMainUI()
    {
        this.Text = "Car Dealership Management System";
    this.Size = new Size(800, 600);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.BackColor = Color.White;

    Label header = new Label();
    header.Text = "Car Dealership Management System";
    header.Font = new Font("Verdana", 24, FontStyle.Bold);
    header.ForeColor = Color.FromArgb(44, 62, 80);
    header.TextAlign = ContentAlignment.MiddleCenter;
    header.Dock = DockStyle.Top;
    header.Height = 80;
    this.Controls.Add(header);

    Panel panel = new Panel();
    panel.Dock = DockStyle.Fill;
    panel.BackColor = Color.FromArgb(236, 240, 241);
    this.Controls.Add(panel);

    FlowLayoutPanel buttonLayout = new FlowLayoutPanel();
    buttonLayout.Dock = DockStyle.None;
    buttonLayout.Size = new Size(300, 400); // Control the size better
    buttonLayout.FlowDirection = FlowDirection.TopDown;
    buttonLayout.WrapContents = false;
    buttonLayout.Location = new Point((panel.Width - buttonLayout.Width) / 2, (panel.Height - buttonLayout.Height) / 2); // Center it
    buttonLayout.Anchor = AnchorStyles.None;
    buttonLayout.Padding = new Padding(10);
    panel.Controls.Add(buttonLayout);

    Button addButton = CreateStyledButton("Add a New Vehicle");
    Button sellButton = CreateStyledButton("Sell a Vehicle");
    Button inventoryButton = CreateStyledButton("View All Vehicles");
    Button salesButton = CreateStyledButton("View Sales History");

    addButton.Width = 250;
    sellButton.Width = 250;
    inventoryButton.Width = 250;
    salesButton.Width = 250;

    addButton.Click += (s, e) => OpenAddVehicleForm();
    sellButton.Click += (s, e) => OpenSellVehicleForm();
    inventoryButton.Click += (s, e) => DisplayInventory();
    salesButton.Click += (s, e) => DisplaySalesHistory();

    buttonLayout.Controls.Add(addButton);
    buttonLayout.Controls.Add(sellButton);
    buttonLayout.Controls.Add(inventoryButton);
    buttonLayout.Controls.Add(salesButton);

    Label footer = new Label();
    footer.Text = "Powered by AutoHub Team | Excellence in Dealership Management";
    footer.Font = new Font("Verdana", 10, FontStyle.Italic);
    footer.ForeColor = Color.FromArgb(127, 140, 141);
    footer.TextAlign = ContentAlignment.MiddleCenter;
    footer.Dock = DockStyle.Bottom;
    footer.Height = 40;
    this.Controls.Add(footer);
    }


    private Button CreateStyledButton(string text)
    {
        Button btn = new Button();
        btn.Text = text;
        btn.Font = new Font("Tahoma", 14, FontStyle.Bold);
        btn.BackColor = Color.FromArgb(52, 152, 219);
        btn.ForeColor = Color.White;
        btn.FlatStyle = FlatStyle.Flat;
        btn.Height = 100;
        btn.Width = 200;
        btn.Margin = new Padding(20);
        btn.Cursor = Cursors.Hand;
        return btn;
    }

    private void OpenAddVehicleForm()
    {
        Form addForm = new Form();
        addForm.Text = "Add a New Vehicle";
        addForm.Size = new Size(400, 300);
        addForm.StartPosition = FormStartPosition.CenterScreen;
        TableLayoutPanel layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 6 };
        layout.Padding = new Padding(10);
        addForm.Controls.Add(layout);

        TextBox makeField = new TextBox();
        TextBox modelField = new TextBox();
        TextBox yearField = new TextBox();
        TextBox colorField = new TextBox();
        TextBox priceField = new TextBox();

        layout.Controls.Add(new Label { Text = "Car Make:" });
        layout.Controls.Add(makeField);
        layout.Controls.Add(new Label { Text = "Car Model:" });
        layout.Controls.Add(modelField);
        layout.Controls.Add(new Label { Text = "Year of Manufacture:" });
        layout.Controls.Add(yearField);
        layout.Controls.Add(new Label { Text = "Color:" });
        layout.Controls.Add(colorField);
        layout.Controls.Add(new Label { Text = "Price (USD):" });
        layout.Controls.Add(priceField);

        Button addButton = CreateStyledButton("Add Vehicle");
        addButton.Click += (s, e) =>
        {
            try
            {
                string make = makeField.Text;
                string model = modelField.Text;
                int year = int.Parse(yearField.Text);
                string color = colorField.Text;
                double price = double.Parse(priceField.Text);
                dealership.AddVehicle(new Car(make, model, year, color, price, "Car"));
                MessageBox.Show("Vehicle successfully added to the inventory!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                addForm.Close();
            }
            catch
            {
                MessageBox.Show("Invalid input. Please enter correct details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };

        layout.Controls.Add(new Label()); // Empty
        layout.Controls.Add(addButton);

        addForm.ShowDialog();
    }

    private void OpenSellVehicleForm()
    {
        Form sellForm = new Form();
        sellForm.Text = "Sell a Vehicle";
        sellForm.Size = new Size(400, 300);
        sellForm.StartPosition = FormStartPosition.CenterScreen;
        TableLayoutPanel layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 4 };
        layout.Padding = new Padding(10);
        sellForm.Controls.Add(layout);

        TextBox indexField = new TextBox();
        TextBox buyerNameField = new TextBox();
        TextBox buyerContactField = new TextBox();

        layout.Controls.Add(new Label { Text = "Vehicle Index (1-Based):" });
        layout.Controls.Add(indexField);
        layout.Controls.Add(new Label { Text = "Customer Name:" });
        layout.Controls.Add(buyerNameField);
        layout.Controls.Add(new Label { Text = "Customer Contact:" });
        layout.Controls.Add(buyerContactField);

        Button sellButton = CreateStyledButton("Confirm Sale");
        sellButton.Click += (s, e) =>
        {
            try
            {
                int index = int.Parse(indexField.Text) - 1;
                string buyerName = buyerNameField.Text;
                string buyerContact = buyerContactField.Text;
                if (dealership.SellVehicle(index, buyerName, buyerContact))
                    MessageBox.Show("Sale completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Invalid index. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                sellForm.Close();
            }
            catch
            {
                MessageBox.Show("Invalid input. Please enter a valid number for the index.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };

        layout.Controls.Add(new Label()); // Empty
        layout.Controls.Add(sellButton);

        sellForm.ShowDialog();
    }

    private void DisplayInventory()
    {
        Form inventoryForm = new Form();
        inventoryForm.Text = "View All Vehicles";
        inventoryForm.Size = new Size(600, 400);
        inventoryForm.StartPosition = FormStartPosition.CenterScreen;

        DataGridView table = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
        table.ColumnCount = 5;
        table.Columns[0].Name = "Make";
        table.Columns[1].Name = "Model";
        table.Columns[2].Name = "Year";
        table.Columns[3].Name = "Color";
        table.Columns[4].Name = "Price (USD)";

        foreach (var v in dealership.Inventory)
        {
            table.Rows.Add(v.Make, v.Model, v.Year, v.Color, v.Price);
        }

        inventoryForm.Controls.Add(table);
        inventoryForm.ShowDialog();
    }

    private void DisplaySalesHistory()
    {
        Form salesForm = new Form();
        salesForm.Text = "View Sales History";
        salesForm.Size = new Size(600, 400);
        salesForm.StartPosition = FormStartPosition.CenterScreen;

        DataGridView table = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
        table.ColumnCount = 5;
        table.Columns[0].Name = "Make";
        table.Columns[1].Name = "Model";
        table.Columns[2].Name = "Buyer";
        table.Columns[3].Name = "Contact";
        table.Columns[4].Name = "Sale Date";

        foreach (var s in dealership.SalesHistory)
        {
            table.Rows.Add(s.Vehicle.Make, s.Vehicle.Model, s.BuyerName, s.BuyerContact, s.SaleDate.ToShortDateString());
        }

        salesForm.Controls.Add(table);
        salesForm.ShowDialog();
    }
}
