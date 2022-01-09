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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace NewAssignment2KIT206
{
    using Controllers;
    using Researchers;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string RESEARCHER_LIST_KEY = "researcherList";
        public Controller controller;

        public MainWindow()
        {
            InitializeComponent();
            controller = (Controller)(Application.Current.FindResource(RESEARCHER_LIST_KEY) as ObjectDataProvider).ObjectInstance;
        }

        private void researcherList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && researcherList.SelectedItem != null)
            {
                DetailsPanel.DataContext = e.AddedItems[0];
            }
        }

        private bool ResearcherFilter(object item)
        {
            if (String.IsNullOrEmpty(researcherFilterTextBox.Text))
                return true;
            else
                return ((item as Researcher).FullName.IndexOf(researcherFilterTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void researcherFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(researcherList.ItemsSource);
            view.Filter = ResearcherFilter;

            CollectionViewSource.GetDefaultView(researcherList.ItemsSource).Refresh();
        }

        private void Ordering_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Ordering.SelectedItem != null)
            {
                string orderDirection = (string)Ordering.SelectedItem;

                if (orderDirection == "New to Old")
                {
                    PublicationList.Items.SortDescriptions.Clear();
                    PublicationList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Available", System.ComponentModel.ListSortDirection.Descending));
                }
                if (orderDirection == "Old to New")
                {
                    PublicationList.Items.SortDescriptions.Clear();
                    PublicationList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Available", System.ComponentModel.ListSortDirection.Ascending));
                }
            }
        }

        private void PublicationList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PublicationList.SelectedItem != null)
            {
                Publication p = (Publication)PublicationList.SelectedItem;
                MessageBox.Show(p.ToDetailedString());
            }
        }

        private bool PublicationLimit(object item)
        {
            if (String.IsNullOrEmpty(LowerLimit.Text) && String.IsNullOrEmpty(UpperLimit.Text))
                return true;
            else
                return ((item as Publication).Available.Year.ToString().CompareTo(LowerLimit.Text) >= 0 && (item as Publication).Available.Year.ToString().CompareTo(UpperLimit.Text) <= 0);
        }

        private void LowerLimit_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PublicationList != null)
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(PublicationList.ItemsSource);
                view.Filter = PublicationLimit;

                CollectionViewSource.GetDefaultView(PublicationList.ItemsSource).Refresh();
            }
        }

        private void UpperLimit_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PublicationList != null)
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(PublicationList.ItemsSource);
                view.Filter = PublicationLimit;

                CollectionViewSource.GetDefaultView(PublicationList.ItemsSource).Refresh();
            }
        }

        private void StarPerformerEmails_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Email(s) copied!");
            string copiedEmail = "";

            foreach (Researcher r in StarPerformers.Items)
            {
                copiedEmail += (r.Email + " ");
            }

            Clipboard.SetText(copiedEmail);
        }

        private void MeetMinimumEmails_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Email(s) copied!");
            string copiedEmail = "";

            foreach (Researcher r in MeetMinimum.Items)
            {
                copiedEmail += (r.Email + " ");
            }

            Clipboard.SetText(copiedEmail);
        }

        private void PoorResearcherEmails_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Email(s) copied!");
            string copiedEmail = "";

            foreach (Researcher r in Poor.Items)
            {
                copiedEmail += (r.Email + " ");
            }

            Clipboard.SetText(copiedEmail);
        }

        private void BelowExpEmails_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Email(s) copied!");
            string copiedEmail = "";

            foreach (Researcher r in BelowExp.Items)
            {
                copiedEmail += (r.Email + " ");
            }

            Clipboard.SetText(copiedEmail);
        }
    }
}
