using System.Data.SqlClient;
using System.Data;
using Spectre.Console;

namespace ProManagement_Assesment
{
    class ProductManagement
    {
        public static void NewProduct(SqlConnection con)
        {
            SqlDataAdapter pro = new SqlDataAdapter("Select * from ProManagement", con);
            DataSet ProDS = new DataSet();
            pro.Fill(ProDS);

            Console.WriteLine("Enter Product Name:");
            string Pname = Console.ReadLine();
            Console.WriteLine("Enter Product Description");
            string descriptions = Console.ReadLine();
            Console.WriteLine("Enter Quantity:");
            int quantity = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Enter Price:");
            int price = Convert.ToInt16(Console.ReadLine());

            var row = ProDS.Tables[0].NewRow();
            row["Product_Name"] = Pname;
            row["Product_Description"] = descriptions;
            row["Quantity"] = quantity;
            row["Price"] = price;

            ProDS.Tables[0].Rows.Add(row);
            SqlCommandBuilder builder = new SqlCommandBuilder(pro);
            pro.Update(ProDS);
            Console.WriteLine("Product saved successfully");
        }

        public static void GetProductById(SqlConnection con)
        {
            Console.WriteLine("Enter Product Id:");
            int Id = Convert.ToInt16(Console.ReadLine());
            SqlDataAdapter pro = new SqlDataAdapter($"Select * from ProManagement where Id = {Id}", con);
            DataSet ProDS = new DataSet();
            pro.Fill(ProDS, "ProTable");

            var table = new Table();
            table.AddColumn("Id");
            table.AddColumn("Product_Name");
            table.AddColumn("Product_Description");
            table.AddColumn("Quantity");
            table.AddColumn("Price");
            for (int i = 0; i < ProDS.Tables[0].Rows.Count; i++)
            {
                for (int j = 0; j < ProDS.Tables[0].Rows.Count; j++)
                {
                    table.AddRow(ProDS.Tables[0].Rows[i][0].ToString(), ProDS.Tables[0].Rows[i][1].ToString(), ProDS.Tables[0].Rows[i][2].ToString(), ProDS.Tables[0].Rows[i][3].ToString(), ProDS.Tables[0].Rows[i][4].ToString());
                }
            }
            AnsiConsole.Write(table);
        }

        public static void ViewAllProducts(SqlConnection con)
        {
            SqlDataAdapter pro = new SqlDataAdapter("Select * from ProManagement", con);
            DataSet ProDS = new DataSet();
            pro.Fill(ProDS, "ProTable");

            var table = new Table();
            table.AddColumn("Id");
            table.AddColumn("Product_Name");
            table.AddColumn("Product_Description");
            table.AddColumn("Quantity");
            table.AddColumn("Price");
            for (int i = 0; i < ProDS.Tables[0].Rows.Count; i++)
            {
                table.AddRow(ProDS.Tables[0].Rows[i][0].ToString(), ProDS.Tables[0].Rows[i][1].ToString(), ProDS.Tables[0].Rows[i][2].ToString(), ProDS.Tables[0].Rows[i][3].ToString(), ProDS.Tables[0].Rows[i][4].ToString());
            }
            AnsiConsole.Write(table);
        }

        public static void UpdateProduct(SqlConnection con)
        {
            Console.WriteLine($"Enter Product Id you want to update: ");
            int Id = Convert.ToInt16(Console.ReadLine());
            SqlDataAdapter pro = new SqlDataAdapter($"Select * from ProManagement where Id = {Id}", con);
            DataSet ProDS = new DataSet();
            pro.Fill(ProDS);

            Console.WriteLine("Enter Product Name:");
            string Pname = Console.ReadLine();
            Console.WriteLine("Enter Product Description");
            string descriptions = Console.ReadLine();
            Console.WriteLine("Enter Quantity:");
            int quantity = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Enter Price:");
            int price = Convert.ToInt16(Console.ReadLine());

            ProDS.Tables[0].Rows[0]["Product_Name"] = Pname;
            ProDS.Tables[0].Rows[0]["Product_Description"] = descriptions;
            ProDS.Tables[0].Rows[0]["Quantity"] = quantity;
            ProDS.Tables[0].Rows[0]["Price"] = price;

            SqlCommandBuilder builder = new SqlCommandBuilder(pro);
            pro.Update(ProDS);
            Console.WriteLine("Product Updated successfully");
        }

        public static void DeleteProduct(SqlConnection con)
        {
            Console.WriteLine("Enter Product Id you want to delete");
            int Id = Convert.ToInt16(Console.ReadLine());
            SqlDataAdapter pro = new SqlDataAdapter($"Select * from ProManagement where Id = {Id}", con);
            DataSet ProDS = new DataSet();
            pro.Fill(ProDS);
            ProDS.Tables[0].Rows[0].Delete();

            SqlCommandBuilder builder = new SqlCommandBuilder(pro);
            pro.Update(ProDS);
            Console.WriteLine("Product deleted successfully");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string ans = "";
            do
            {

                SqlConnection con = new SqlConnection("Server= US-5HSQ8S3; database=ProManage; Integrated Security=true");
                AnsiConsole.Write(new FigletText("Welcome to Product Management App...").LeftJustified().Color(Color.Blue));
                Console.WriteLine("1. Add New Product");
                Console.WriteLine("2. Get Product");
                Console.WriteLine("3. View All Products");
                Console.WriteLine("4. Update Product By Id");
                Console.WriteLine("5. Delete Product By Id");
                try
                {
                    int choice = Convert.ToInt16(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            {
                                ProductManagement.NewProduct(con);
                                break;
                            }
                        case 2:
                            {
                                ProductManagement.GetProductById(con);
                                break;
                            }
                        case 3:
                            {
                                ProductManagement.ViewAllProducts(con);
                                break;
                            }
                        case 4:
                            {
                                ProductManagement.UpdateProduct(con);
                                break;
                            }
                        case 5:
                            {
                                ProductManagement.DeleteProduct(con);
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Wrong choice entered");
                                break;
                            }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Only numbers are allowed");
                }
                Console.WriteLine("Do you wish to continue? [If yes, type y] ");
                ans = Console.ReadLine();
            } while (ans.ToLower() == "y");
        }
    }
}