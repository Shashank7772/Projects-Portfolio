using System;
using System.Numerics;
using MySql.Data.MySqlClient;

class Program
{
    static void ExecuteUpdate(MySqlConnection connection, BigInteger productBarcode, string columnName, object newValue)
    {
        string updateQuery = $"UPDATE products SET {columnName} = @newValue WHERE ProductBarcode = @productBarcode";

        using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
        {
            updateCommand.Parameters.AddWithValue("@newValue", newValue);
            updateCommand.Parameters.AddWithValue("@productBarcode", productBarcode);
            updateCommand.ExecuteNonQuery();
            Console.WriteLine($"{columnName} updated successfully.");
        }
    }

    static void Main()
    {
        string? userInput0;
        string? userInput1;
        string menuSelection = "";
        Console.Clear();

        //To create a connection with the database 
        Console.Write("Enter you Login ID : ");
        userInput0 = Console.ReadLine();
        Console.Write("Enter your Password : ");
        userInput1 = Console.ReadLine();

        //Function to verify the user credentials
        static bool verifyUserCredentials(string loginID, string password)
        {
            return loginID == "sqluser" && password == "password";
        }

        bool isAuthenticated = verifyUserCredentials(userInput0, userInput1);

        if (!isAuthenticated)
        {
            Console.WriteLine("Invalid Login credentils");
            return;
        }
        else
        {
            //Now if the user gets authenticated proceed with establishing the connection with database
            string connectionString = "Server=localhost;Database=dksupermarket;Uid=" + userInput0 + ";Pwd=" + userInput1 + ";";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection has been succesfully established");
                    Console.ReadKey();
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("1. View the list of all items.");
                        Console.WriteLine("2. Enter a new product.");
                        Console.WriteLine("3. Edit an existing product.");
                        Console.WriteLine("4. Delete an existing prodcut.");
                        Console.WriteLine("5. Check all the products for expiration.\n");
                        Console.WriteLine("Enter your selection number (close the application by typing \"close\" word)");

                        //This section takes the decision based off of user's choice
                        userInput0 = Console.ReadLine();
                        if (userInput0 != null)
                        {
                            menuSelection = userInput0.Trim().ToLower();
                        }
                        Console.WriteLine($"You entered the choice {menuSelection}\nPress any key to continue\n");
                        //pauses the execution of program
                        Console.ReadKey();

                        //This part of the program uses switch case to perform the functionalities displayed above
                        switch (menuSelection)
                        {
                            case "1":
                                {
                                    // Select all items from the products table
                                    string selectQuery = "SELECT * FROM products";

                                    MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection);

                                    using (MySqlDataReader reader = selectCommand.ExecuteReader())
                                    {
                                        // Display the column headers
                                        Console.WriteLine("{0,-15} {1,-30} {2,-20} {3,-18} {4,-18} {5,-15} {6,-15} {7,-15}",
                                            "ProductBarcode", "ProductName", "ManufacturingDate", "BestBeforeMonths", "ExpiryDate", "PurchasePrice", "TaxSlab", "SellingPrice");

                                        // Display each row of data
                                        while (reader.Read())
                                        {
                                            Console.WriteLine("{0,-15} {1,-30} {2,-20:MM/dd/yyyy} {3,-18} {4,-18:MM/dd/yyyy} {5,-15} {6,-15} {7,-15}",
                                            reader["ProductBarcode"], reader["ProductName"], reader["ManufacturingDate"],
                                            reader["BestBefore"], reader["ExpiryDate"], reader["PurchasePrice"],
                                            reader["TaxSlab"], reader["SellingPrice"]);
                                        }
                                        reader.Close();
                                    }

                                    Console.ReadKey();
                                    break;
                                }

                            case "2":
                                {
                                    //All the declared variable to store the user information first in memory
                                    Console.Write("Enter the barcode of the product : ");
                                    string productBarcode = Console.ReadLine();
                                    Console.Write("Enter the full name of the product here : ");
                                    string productName = Console.ReadLine();
                                    Console.Write("Enter the manufacturing date of the product (YYYY/MM/DD) : ");
                                    string productMfd = Console.ReadLine();
                                    Console.Write("Enter the best before period (in months) : ");
                                    int bestBefore = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("Enter the Purchase Price: ");
                                    decimal purchasePrice = Convert.ToDecimal(Console.ReadLine());
                                    Console.Write("Tax Slab (e.g., 0.1 for 10% tax ) : ");
                                    decimal taxSlab = Convert.ToDecimal(Console.ReadLine());

                                    //Insert the new item into the database
                                    string insertQuery = $"INSERT INTO products (ProductBarcode, ProductName, ManufacturingDate, BestBefore, PurchasePrice, TaxSlab) " +
                                                            $"VALUES ('{productBarcode}', '{productName}', '{productMfd}', {bestBefore}, {purchasePrice}, {taxSlab})";

                                    //After the connection, new SQL commands are made whenever interacting with the table
                                    MySqlCommand command = new MySqlCommand(insertQuery, connection);
                                    int rowsAffected = command.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        Console.WriteLine("New product added successfully!");
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error adding product. Please try again.");
                                        Console.ReadKey();
                                    }

                                    break;
                                }
                            case "3":
                                {
                                    Console.WriteLine("New details for an existing product are entered through this part");
                                    Console.Write("Enter the barcode of the product you want to edit : ");
                                    string barcodeToSearch = Console.ReadLine();

                                    string selectQuery = $"SELECT * FROM products WHERE ProductBarcode = '{barcodeToSearch}'";
                                    using MySqlCommand command = new MySqlCommand(selectQuery, connection);
                                    {

                                        using (MySqlDataReader reader = command.ExecuteReader())
                                        {
                                            if (reader.HasRows)
                                            {
                                                Console.WriteLine("Product Found");
                                                Console.ReadKey();

                                                while (reader.Read())
                                                {
                                                    long _productBarcode = reader.GetInt64("ProductBarcode");
                                                    string _productName = reader.GetString("ProductName");
                                                    decimal _purchasePrice = reader.GetDecimal("PurchasePrice");
                                                    decimal _taxSlab = reader.GetDecimal("TaxSlab");
                                                    DateTime _productMfd = reader.GetDateTime("ManufacturingDate");

                                                    //This displays the current information about the product searched
                                                    Console.WriteLine($"Product Barcode : {_productBarcode}");
                                                    Console.WriteLine($"Product Name : {_productName}");
                                                    Console.WriteLine($"Product purchase price : {_purchasePrice}");
                                                    Console.WriteLine($"Product tax slab : {_taxSlab}");
                                                    Console.WriteLine($"Manufacturing Date: {_productMfd.ToShortDateString()}");

                                                    //This display's the options from which the user can choose what he wants to update
                                                    Console.WriteLine("Enter the field you want to update : ");
                                                    Console.WriteLine("1. Product Name");
                                                    Console.WriteLine("2. Product purchase price");
                                                    Console.WriteLine("3. Product tax slab");
                                                    Console.WriteLine("4. Manufacturing Date");
                                                    Console.WriteLine("5. Go Back");

                                                    userInput0 = Console.ReadLine();

                                                    //This is executed based on user's choice and executes as the name suggests
                                                    switch (userInput0)
                                                    {
                                                        case "1":
                                                            {
                                                                Console.Write("Enter new product name : ");
                                                                string newName = Console.ReadLine();
                                                                reader.Close();
                                                                ExecuteUpdate(connection, _productBarcode, "ProductName", newName);
                                                                break;
                                                            }
                                                        case "2":
                                                            {
                                                                Console.Write("Enter the new purchase price : ");
                                                                decimal newPrice = Convert.ToDecimal(Console.ReadLine());
                                                                reader.Close();
                                                                ExecuteUpdate(connection, _productBarcode, "PurchasePrice", newPrice);
                                                                break;
                                                            }
                                                        case "3":
                                                            {
                                                                Console.Write("Enter new Product tax slab : ");
                                                                decimal newTaxSlab = Convert.ToDecimal(Console.ReadLine());
                                                                reader.Close();
                                                                ExecuteUpdate(connection, _productBarcode, "TaxSlab", newTaxSlab);
                                                                break;
                                                            }
                                                        case "4":
                                                            {
                                                                Console.Write("Enter the new Manufacturing Date : ");
                                                                DateTime newManufacturingDate = Convert.ToDateTime(Console.ReadLine());
                                                                reader.Close();
                                                                ExecuteUpdate(connection, _productBarcode, "ManufacturingDate", newManufacturingDate);
                                                                break;
                                                            }
                                                        default:
                                                            {
                                                                Console.WriteLine("Invalid Choice");
                                                                reader.Close();
                                                                break;
                                                            }
                                                    }
                                                    Console.ReadKey();
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Product now found");
                                                Console.ReadKey();
                                            }
                                        }
                                    }
                                    break;
                                }
                            case "4":
                                {
                                    Console.WriteLine("Deleting an existing product.");
                                    Console.Write("Enter the barcode of the product you want to delete : ");
                                    string barcodeToDelete = Console.ReadLine();

                                    string deleteQuery = $"DELETE FROM products WHERE ProductBarcode = '{barcodeToDelete}'";
                                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);

                                    int rowsAffected = deleteCommand.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        Console.WriteLine("Product deleted successfully!");
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Product not found or error deleting product.");
                                        Console.ReadKey();
                                    }

                                    break;
                                }
                            case "5":
                                {
                                    Console.WriteLine("Checking for products with less than 1 month remaining before expiry...");
                                    Console.ReadKey();

                                    DateTime today = DateTime.Today;

                                    // Select products with expiry date less than (today + 1 month)
                                    string selectQuery = $"SELECT * FROM products WHERE ExpiryDate < DATE_ADD(CURDATE(), INTERVAL 1 MONTH)";

                                    MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection);

                                    using (MySqlDataReader reader = selectCommand.ExecuteReader())
                                    {
                                        // Display the products with less than 1 month remaining before expiry
                                        Console.WriteLine("{0,-15} {1,-30} {2,-18} {3,-18}",
                                            "ProductBarcode", "ProductName", "ExpiryDate", "DaysRemaining");

                                        while (reader.Read())
                                        {
                                            long _productBarcode = reader.GetInt64("ProductBarcode");
                                            string _productName = reader.GetString("ProductName");
                                            DateTime _expiryDate = reader.GetDateTime("ExpiryDate");

                                            TimeSpan timeRemaining = _expiryDate - today;
                                            int daysRemaining = timeRemaining.Days;

                                            if (daysRemaining < 30)
                                            {
                                                Console.WriteLine("{0,-15} {1,-30} {2,-18:MM/dd/yyyy} {3,-18}",
                                                    _productBarcode, _productName, _expiryDate, daysRemaining);
                                            }
                                            else if(daysRemaining<0)
                                            {
                                                Console.WriteLine("These products have already expired");
                                                 Console.WriteLine("{0,-15} {1,-30} {2,-18:MM/dd/yyyy} {3,-18}",
                                                    _productBarcode, _productName, _expiryDate, daysRemaining);
                                            }
                                        }

                                        reader.Close();

                                    }

                                    Console.ReadKey();
                                    break;
                                }

                            //This will be used to exit out of program manually
                            case "close":
                                break;

                            //To make sure the user enters a valid number
                            default:
                                {
                                    Console.WriteLine("Please enter a valid number.");
                                    Console.ReadKey();
                                    break;
                                }
                        }

                    } while (menuSelection != "close");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hello There : " + ex.Message);
                    Console.ReadKey();
                    return;
                }
            }
        }
    }
}
