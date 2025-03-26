using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _2025_03_20
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        ServerConnection connection;
        public Window1(ServerConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            Start();
        }
        async void Start()
        {
            List<string> names = await connection.AllName();
            List<string> ages = await connection.AllAge();

            for (int i = 0; i < names.Count; i++)
            {
                string name = names[i];
                string age = ages[i];

                TextBlock nameTextBlock = new TextBlock
                {
                    Text = name,
                    FontSize = 20,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                NameStack.Children.Add(nameTextBlock);

                TextBlock ageTextBlock = new TextBlock
                {
                    Text = age,
                    FontSize = 20,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                AgeStack.Children.Add(ageTextBlock);

                Button deleteB = new Button
                {
                    Content = "X",
                    FontSize = 10,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 10, 0, 0)
                };
                Button editbutton = new Button
                {
                    Content = "Edit",
                    FontSize = 10,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(10, 10, 0, 0)
                };
                deleteB.Click += async (s, e) =>
                {
                    bool result = await connection.DeletePerson(name);
                    if (result)
                    {
                        NameStack.Children.Remove(nameTextBlock);
                        AgeStack.Children.Remove(ageTextBlock);
                        deleteButtonStack.Children.Remove(deleteB);
                    }
                    else
                    {
                        MessageBox.Show("Error deleting the person.");
                    }
                };

                deleteButtonStack.Children.Add(deleteB);
                deleteButtonStack.Children.Add(editbutton);
            }




        }

        async void createPerson(object s, EventArgs e)
        {
            bool valami = await connection.create(NameInput.Text, Convert.ToInt32(AgeInput.Text));
            if (valami)
            {

                MessageBox.Show("created person");
                NameStack.Children.Clear();
                AgeStack.Children.Clear();
                deleteButtonStack.Children.Clear();
                Start();
            }
            else
            {
                MessageBox.Show("Nem jó valami");
            }
        }
        async void deleteallperson(object s, EventArgs e)
        {
            bool valami = await connection.deleteAll();
            if (valami)
            {
                MessageBox.Show("Mindenki kitorolve");
                NameStack.Children.Clear();
                AgeStack.Children.Clear();
                deleteButtonStack.Children.Clear();
                Start();

            }
        }
    }
}
