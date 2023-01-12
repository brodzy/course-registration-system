using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hw08_brodzinski
{
    public partial class _default : System.Web.UI.Page
    {
        private ListItem[] buildAvailableCourseList()
        {
            ListItem[] tempList = { 
                                new ListItem("CS 1301-4", "CS 1301-4"),
                                new ListItem("CS 1302-4", "CS 1302-4"),
                                new ListItem("CS 1303-4", "CS 1303-4"),
                                new ListItem("CS 2202-2", "CS 2202-2"),
                                new ListItem("CS 2224-2", "CS 2224-2"),
                                new ListItem("CS 3300-3", "CS 3300-3"),
                                new ListItem("CS 3301-1", "CS 3301-1"),
                                new ListItem("CS 3302-1", "CS 3302-1"),
                                new ListItem("CS 3340-3", "CS 3340-3"),
                                new ListItem("CS 4321-3", "CS 4321-3"),
                                new ListItem("CS 4322-3", "CS 4322-3")
                              };
            return tempList;
        }

        private int ExtraCosts()
        {
            int total = 0;
            //Loop over checkbox and add to total
            foreach (ListItem li in checkBoxListAddOn.Items)
            {
                if (li.Selected)
                {
                    var price = Int32.Parse(li.Value);
                    total += price;
                }   
            }
            return total;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)        //For initial page creation
            {
                ListItem[] availableCourses = buildAvailableCourseList();
                lbxAvailableClasses.DataSource = availableCourses;
                lbxAvailableClasses.DataTextField = "Text";
                lbxAvailableClasses.DataValueField = "Value";
                lbxAvailableClasses.DataBind();
            }
        }

        protected void addBtn_Click(object sender, EventArgs e)
        {
          
            //List of selected courses and keeping track of totals/costs
            List<ListItem> removedItems = new List<ListItem>();
            var total = Int32.Parse(lblHours.Text);
            var cost = 0;
            int addOnCost = ExtraCosts();

            //Loop over selected courses and add to registered courses
            foreach (ListItem item in lbxAvailableClasses.Items)
            {
                if (item.Selected)
                {
                    var hours = item.Value;
                    var result = Int16.Parse(hours.Split('-').Last());

                    if (total + result <= 19)
                    {
                        lblAlert.Visible = false;
                        //Updating cost and hours amount
                        total += result;
                        cost = total * 100;
                        
                        lblHours.Text = total.ToString();     

                        item.Selected = false;
                        lbxRegisteredClasses.Items.Add(item);
                        removedItems.Add(item);
                    }
                    else
                    {
                        lblAlert.Visible = true;
                        lblAlert.Text = "You cannot register for more than 19 hours.";
                    }
                   
                }
                else
                {
                    cost = total * 100;
                }
            }

            //Increases/Decreases cost label for all checkboxes that are selected
            int combinedTotal = addOnCost + cost;
            lblCost.Text = "$" + combinedTotal.ToString() + ".00";

            //Remove items from avilable courses
            foreach (ListItem item in removedItems)
            {
                lbxAvailableClasses.Items.Remove(item);
            }


        }

        protected void removeBtn_Click(object sender, EventArgs e)
        {
            lblAlert.Visible = false;
            var total = Int16.Parse(lblHours.Text);
            var cost = 0;
            int addOnCost = ExtraCosts();

            //List of selected courses
            List<ListItem> removedItems = new List<ListItem>();

            //Loop over selected courses and add back to avilable courses
            foreach (ListItem item in lbxRegisteredClasses.Items)
            {
                if (item.Selected)
                {
                    //Updating cost and hours amount
                    var hours = item.Value;
                    var result = Int16.Parse(hours.Split('-').Last());
                    total -= result;
                    cost = total * 100;

                    lblHours.Text = total.ToString();

                    item.Selected = false;
                    lbxAvailableClasses.Items.Add(item);
                    removedItems.Add(item);
                }
                else
                {
                    cost = total * 100;
                }
            }

            //Increases/Decreases cost label for all checkboxes that are selected
            int combinedTotal = addOnCost + cost;
            lblCost.Text = "$" + combinedTotal.ToString() + ".00";

            //Remove items from registered courses
            foreach (ListItem item in removedItems)
            {
                lbxRegisteredClasses.Items.Remove(item);
            }
        }
        

        protected void resetBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void makeAval_Click(object sender, EventArgs e)
        {
            //Adding class to available classes 
            var classNum = classNumBox.Text;
            var upperClassNum = classNum.ToUpper();
            var classCredit = creditsBox.Text;
            List<String> names = new List<String>();

            //Adding all available and registered course names to one list
            foreach (ListItem i in lbxAvailableClasses.Items)
            {
                var avalResult = i.Value.Split('-').First();
                names.Add(avalResult);
            }

            foreach (ListItem i in lbxRegisteredClasses.Items)
            {
                var regResult = i.Value.Split('-').First();
                names.Add(regResult);
            }

            //Checking if input matches current list values
            if (names.Contains(upperClassNum))
            {
                lblExists.Visible = true;
                lblExists.Text = "Not added. Course already exists.";
            }
            else
            {
                var course = classNum + "-" + classCredit;
                ListItem item = new ListItem(course, course);
                lbxAvailableClasses.Items.Add(item);
                lblExists.Visible = true;
                lblExists.Text = "Added successfully,";
            }
        }

        protected void removeAval_Click(object sender, EventArgs e)
        {
            var classNum = classNumBox.Text;
            List<ListItem> matchedItems = new List<ListItem>();
            List<String> names = new List<String>();

            //Adding all available and registered course names to one list
            foreach (ListItem i in lbxAvailableClasses.Items)
            {
                var avalResult = i.Value.Split('-').First();
                names.Add(avalResult);
            }

            foreach (ListItem i in lbxRegisteredClasses.Items)
            {
                var regResult = i.Value.Split('-').First();
                names.Add(regResult);
            }

            //Checking if input matches current list values
            if (!names.Contains(classNum))
            {
                lblExists.Visible = true;
                lblExists.Text = "Course not found";
            }

            //Adding matched class to separate list
            foreach (ListItem i in lbxAvailableClasses.Items)
            {
                var avalResult = i.Value.Split('-').First();
                if(String.Equals(classNum, avalResult))
                {
                    matchedItems.Add(i);
                }
            }
            
            //Removing matched class from available courses
            foreach(ListItem i in matchedItems)
            {
                lbxAvailableClasses.Items.Remove(i);
                lblExists.Visible = true;
                lblExists.Text = "Removed successfully.";
            }

            //Checks for classes that are registered before removal
            foreach (ListItem i in lbxRegisteredClasses.Items)
            {
                var regResult = i.Value.Split('-').First();
                if (String.Equals(classNum, regResult))
                {
                    lblExists.Visible = true;
                    lblExists.Text = "Not removed. Course is registered for.";
                }

            }

        }
    }

}