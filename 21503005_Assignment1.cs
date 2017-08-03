using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// IT5x84 - Programming
/// ASSIGNMENT 1
/// STUDENT NAME: Patricia Nellas
/// STUDENT ID: 21503005
/// DATE: 4/09/2016
/// TUTOR: Iwan Tjhin 

namespace Assignment1
{
    public partial class frmSalesSystem : Form
    {
        public frmSalesSystem()
        {
            InitializeComponent();
        }

        //GLOBAL VARIABLES
        decimal d_total;
        decimal d_shoestotal = 0;
        decimal d_jacketstotal = 0;
        decimal d_glovestotal = 0;
        decimal d_beaniestotal = 0;
        decimal d_sweaterstotal = 0;
        decimal d_scarvestotal = 0;
        decimal d_shoesunitprice;
        decimal d_shoesquantity;
        decimal d_jacketsunitprice;
        decimal d_jacketsquantity;
        decimal d_glovesunitprice;
        decimal d_glovesquantity;
        decimal d_beaniesunitprice;
        decimal d_beaniesquantity;
        decimal d_sweatersunitprice;
        decimal d_sweatersquantity;
        decimal d_scarvesunitprice;
        decimal d_scarvesquantity;

        private void frmSalesSystem_Load(object sender, EventArgs e)
        {

            //Card Type combo box
            cmbCardType.Items.Add("Visa");
            cmbCardType.Items.Add("Mastercard");
            cmbCardType.Items.Add("Diner's Club");
            cmbCardType.Items.Add("American Express");

            //Expiry Month combo box
            cmbExpiryMonth.Items.Add("January");
            cmbExpiryMonth.Items.Add("February");
            cmbExpiryMonth.Items.Add("March");
            cmbExpiryMonth.Items.Add("April");
            cmbExpiryMonth.Items.Add("May");
            cmbExpiryMonth.Items.Add("June");
            cmbExpiryMonth.Items.Add("July");
            cmbExpiryMonth.Items.Add("August");
            cmbExpiryMonth.Items.Add("September");
            cmbExpiryMonth.Items.Add("October");
            cmbExpiryMonth.Items.Add("November");
            cmbExpiryMonth.Items.Add("December");

            //Expiry Year Combo Box LOOP
            //dynamically sets the items in the combo box
            int maxYear = DateTime.Now.Year + 10;
            for (int year = DateTime.Now.Year; year <= maxYear; year++)
            {
                cmbExpiryYear.Items.Add(year.ToString());
            }

            //Issuing Bank combo box
            cmbIssuingBank.Items.Add("New Zealand");
            cmbIssuingBank.Items.Add("Overseas");

            //Initial State of Credit Card radiobutton 
            //and the Credit Card Info groupbox
            radCreditCard.Enabled = false;
            grpCreditCardInfo.Enabled = false;

            //Set Prices for Products
            txtShoesUnitPrice.Text = "39.99";
            txtJacketsUnitPrice.Text = "59.99";
            txtGlovesUnitPrice.Text = "20.00";
            txtBeaniesUnitPrice.Text = "24.99";
            txtSweatersUnitPrice.Text = "69.99";
            txtScarvesUnitPrice.Text = "24.99";
            txtTotalAmount.Text = "0.00";

        }

        private void radCash_CheckedChanged(object sender, EventArgs e)
        {
            //disables Credit Card Info groupbox when Cash radiobutton is checked
            grpCreditCardInfo.Enabled = false; 
        }

        private void radCreditCard_CheckedChanged(object sender, EventArgs e)
        {
            //enables Credit Card Info groupbox when Credit Card radiobutton is checked
            grpCreditCardInfo.Enabled = true;
        }

        private void radBankTransfer_CheckedChanged(object sender, EventArgs e)
        {
            //disables Credit Card Info groupbox when Cash radiobutton is checked
            grpCreditCardInfo.Enabled = false;
        }

        private void btnShowSummary_Click(object sender, EventArgs e)
        {
            //boolean variables for error checking
            bool boolCreditCheck = false;
            bool boolFirstLastNameCheck = true;
            bool boolItemCheck = false;

            //------ERROR CHECKING------
            //Renders First Name and Last Name fields mandatory
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) && string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("First name and last name field required", "ERROR");
                boolFirstLastNameCheck = false;
            }
            else if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {

                MessageBox.Show("First name field required", "ERROR");
                boolFirstLastNameCheck = false;
            }
            else if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Last name field required", "ERROR");
                boolFirstLastNameCheck = false;
            }

            //------ERROR CHECKING------
            //Ensures the First Name and Last Name input are within the minimum/maximum length
            if (boolFirstLastNameCheck == true)
            {
                if (txtFirstName.TextLength <= 3)
                {
                    MessageBox.Show("First name input too short.", "ERROR");
                    boolFirstLastNameCheck = false;
                }
                else if (txtFirstName.TextLength > 20)
                {
                    MessageBox.Show("First name input too long.", "ERROR");
                    boolFirstLastNameCheck = false;
                }

                if (txtLastName.TextLength <= 3)
                {
                    MessageBox.Show("Last name input too short.", "ERROR");
                    boolFirstLastNameCheck = false;
                }
                else if (txtLastName.TextLength > 20)
                {
                    MessageBox.Show("Last name input too long.", "ERROR");
                    boolFirstLastNameCheck = false;
                }
            }

            //------ERROR CHECKING------
            //Renders all the fields in the Credit Card Info groupbox mandatory
            if (radCreditCard.Checked == true)
            {
                if (string.IsNullOrWhiteSpace(txtNameOnCard.Text) | string.IsNullOrWhiteSpace(cmbCardType.Text) |
                    string.IsNullOrWhiteSpace(txtCardNumber.Text) | string.IsNullOrWhiteSpace(cmbExpiryMonth.Text) |
                    string.IsNullOrWhiteSpace(cmbExpiryYear.Text) | string.IsNullOrWhiteSpace(cmbIssuingBank.Text))
                {
                    MessageBox.Show("All fields in the Credit Card Information section are mandatory.", "ERROR");
                }
                else
                {
                    boolCreditCheck = true;
                }
            }

            //------ERROR CHECKING------
            //Ensures there is at least one output withint the Product Selection Groupbox
            if (chkShoes.Checked == false && chkJackets.Checked == false && chkGloves.Checked == false &&
                chkBeanies.Checked == false && chkSweaters.Checked == false && chkScarves.Checked == false)
            {
                MessageBox.Show("Minimum of one product should be selected with respective quantity.", "ERROR");
                boolItemCheck = false;
            }
            else if (txtShoesQuantity.Text == "" && txtJacketsQuantity.Text == "" && txtGlovesQuantity.Text == "" &&
                txtBeaniesQuantity.Text == "" && txtSweatersQuantity.Text == "" && txtGlovesQuantity.Text == "")
            {
                MessageBox.Show("Minimum of one product should be selected with respective quantity.", "ERROR");
            }
            else
            {
                boolItemCheck = true;
            }

            //------ERROR CHECKING------
            //CREDIT CARD RADIOBUTTON
            if (radCreditCard.Checked == true)
            {
                
                if (boolCreditCheck == true)
                {
                    //ERROR CHECKING FOR THE CARD TYPE
                    try
                    {
                        switch (cmbCardType.Text)
                        {
                            case "Visa": break;
                            case "Mastercard": break;
                            case "Diner's Club": break;
                            case "American Express": break;
                            case "": break;
                            default: boolCreditCheck = false; MessageBox.Show("Card Type invalid.", "ERROR"); break;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Card Type invalid.", "ERROR");
                        boolCreditCheck = false;
                    }
                    
                    //ERROR CHECKING FOR THE EXPIRY MONTH
                    try
                    {
                        switch (cmbExpiryMonth.Text)
                        {
                            case "January": break;
                            case "February": break;
                            case "March": break;
                            case "April": break;
                            case "May": break;
                            case "June": break;
                            case "July": break;
                            case "August": break;
                            case "September": break;
                            case "October": break;
                            case "November": break;
                            case "December": break;
                            case "": break;
                            default: boolCreditCheck = false; MessageBox.Show("Expiry month invalid.", "ERROR"); break;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Expiry month invalid.", "ERROR");
                        boolCreditCheck = false;
                    }

                    //ERROR CHECKING FOR THE EXPIRY YEAR
                    try
                    {
                        int i_currentYear = DateTime.Now.Year;
                        int i_maxYear = DateTime.Now.Year + 10;
                        decimal d_year = decimal.Parse(cmbExpiryYear.Text);

                        if (d_year >= i_currentYear && d_year <= i_maxYear)
                        { }
                        else
                        {
                            MessageBox.Show("Expiry year invalid.", "ERROR");
                            boolCreditCheck = false;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Expiry year invalid.", "ERROR");
                        boolCreditCheck = false;
                    }

                    //ERROR CHECKING FOR ISSUING BANK
                    try
                    {
                        switch (cmbIssuingBank.Text)
                        {
                            case "New Zealand": break;
                            case "Overseas": break;
                            case "": break;
                            default: boolCreditCheck = false; MessageBox.Show("Issuing Bank invalid.", "ERROR"); break;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Issuing Bank invalid.", "ERROR");
                        boolCreditCheck = false;
                    }
                }
            }


            //------SHOW SUMMARY------
            if (boolFirstLastNameCheck == true && boolItemCheck == true)
            {
                if (radCash.Checked == false && radCreditCard.Checked == false && radBankTransfer.Checked == false)
                {
                    //Ensures an Payment Type radiobutton is checked before proceeding
                    MessageBox.Show("Payment type required to proceed.", "ERROR");
                }
                else
                {
                    //string variables declared for the Customer Details information
                    string strFirstName = txtFirstName.Text;
                    string strLastName = txtLastName.Text;
                    string strAddress = txtAddress.Text;
                    string strSuburb = "";
                    string strCity = "";
                    string strCountry = "";
                    //string variables declared for the product quantities and total amount
                    string strShoes = "";
                    string strJackets = "";
                    string strGloves = "";
                    string strBeanies = "";
                    string strSweaters = "";
                    string strScarves = "";
                    string strTotal = txtTotalAmount.Text;
                    //checks whether specific product is enabled before it is assigned to the variable
                    if (txtShoesQuantity.Enabled == true) { strShoes = txtShoesQuantity.Text; }
                    if (txtJacketsQuantity.Enabled == true) { strJackets = txtJacketsQuantity.Text; }
                    if (txtGlovesQuantity.Enabled == true) { strGloves = txtGlovesQuantity.Text; }
                    if (txtBeaniesQuantity.Enabled == true) { strBeanies = txtBeaniesQuantity.Text; }
                    if (txtSweatersQuantity.Enabled == true) { strSweaters = txtSweatersQuantity.Text; }
                    if (txtScarvesQuantity.Enabled == true) { strScarves = txtScarvesQuantity.Text; }

                    //---SUMMARY---
                    //Credit Card radiobutton
                    if (radCreditCard.Checked == true)
                    {
                        if (boolCreditCheck == true)
                        {
                            MessageBox.Show("CUSTOMER DETAILS" + Environment.NewLine + "First Name: " +
                                strFirstName + Environment.NewLine + "Last Name: " + strLastName + Environment.NewLine +
                                "Address: " + strAddress + Environment.NewLine + "Suburb: " + strSuburb +
                                Environment.NewLine + "City: " + strCity + Environment.NewLine + "Country: " +
                                strCountry + Environment.NewLine + Environment.NewLine + Environment.NewLine + "PRODUCT SELECTION" +
                                Environment.NewLine + "Shoes: " + strShoes + Environment.NewLine + "Jackets: " + strJackets +
                                Environment.NewLine + "Gloves: " + strGloves + Environment.NewLine + "Beanies: " + strBeanies +
                                Environment.NewLine + "Sweaters: " + strSweaters + Environment.NewLine + "Scarves: " + strScarves +
                                Environment.NewLine + Environment.NewLine + "PAYMENT TYPE: Credit Card" + Environment.NewLine +
                                "TOTAL AMOUNT: $" + strTotal, "SUMMARY");
                        }
                    }
                    //Cash and Bank Transfer radiobuttons
                    if (radCash.Checked == true | radBankTransfer.Checked == true)
                    {
                        string strPaymentType = "";
                        if (radCash.Checked == true)
                        { strPaymentType = "Cash"; }
                        if (radBankTransfer.Checked == true)
                        { strPaymentType = "Bank Transfer"; }

                        MessageBox.Show("CUSTOMER DETAILS" + Environment.NewLine + "First Name: " +
                                strFirstName + Environment.NewLine + "Last Name: " + strLastName + Environment.NewLine +
                                "Address: " + strAddress + Environment.NewLine + "Suburb: " + strSuburb +
                                Environment.NewLine + "City: " + strCity + Environment.NewLine + "Country: " +
                                strCountry + Environment.NewLine + Environment.NewLine + Environment.NewLine + "PRODUCT SELECTION" +
                                Environment.NewLine + "Shoes: " + strShoes + Environment.NewLine + "Jackets: " + strJackets +
                                Environment.NewLine + "Gloves: " + strGloves + Environment.NewLine + "Beanies: " + strBeanies +
                                Environment.NewLine + "Sweaters: " + strSweaters + Environment.NewLine + "Scarves: " + strScarves +
                                Environment.NewLine + Environment.NewLine + "PAYMENT TYPE: " + strPaymentType + Environment.NewLine +
                                "TOTAL AMOUNT: $" + strTotal, "SUMMARY");
                    }
                }  
            }
        }
       

        private void btnReset_Click(object sender, EventArgs e)
        {//------RESETS APPLICATION TO STARTUP STATE------
            
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAddress.Text = "";
            txtSuburb.Text = "";
            txtCity.Text = "";
            txtCountry.Text = "";

            
            txtNameOnCard.Text = "";
            cmbCardType.Text = "";
            txtCardNumber.Text = "";
            cmbExpiryMonth.Text = "";
            cmbExpiryYear.Text = "";
            cmbIssuingBank.Text = "";

            chkShoes.Checked = false;
            chkJackets.Checked = false;
            chkGloves.Checked = false;
            chkBeanies.Checked = false;
            chkSweaters.Checked = false;
            chkScarves.Checked = false;
            txtShoesQuantity.Text = "";
            txtJacketsQuantity.Text = "";
            txtGlovesQuantity.Text = "";
            txtBeaniesQuantity.Text = "";
            txtSweatersQuantity.Text = "";
            txtScarvesQuantity.Text = "";

            radCash.Enabled = true;
            radCreditCard.Enabled = false;
            radBankTransfer.Enabled = true;
            txtTotalAmount.Text = "0.00";

            if (radCash.Checked == true)
            { radCash.Checked = false; }
            if (radCreditCard.Checked == true)
            { radCreditCard.Checked = false; }
            if (radBankTransfer.Checked == true)
            { radBankTransfer.Checked = false; }

            if (grpCreditCardInfo.Enabled == true)
            {
                grpCreditCardInfo.Enabled = false;
            }
        }

        private void chkShoes_CheckedChanged(object sender, EventArgs e)
        {//------SHOES------
            //Statement if Shoes checkbox is ticked
            if (chkShoes.Checked == true)
            {
                txtShoesQuantity.Enabled = true;

                try
                {
                    if (chkShoes.Checked)
                    {
                        //assignation to variables
                        d_shoesunitprice = decimal.Parse(txtShoesUnitPrice.Text);
                        if (string.IsNullOrEmpty(txtShoesQuantity.Text))
                        { d_shoesquantity = 0; }
                        else { d_shoesquantity = decimal.Parse(txtShoesQuantity.Text); }

                        //initial calculation
                        d_shoestotal = d_shoesunitprice * d_shoesquantity;
                    }
                    else
                    {
                        d_shoestotal = 0;
                    }
                    //Total Amount calculation
                    d_total = d_shoestotal + d_jacketstotal + d_glovestotal + d_beaniestotal + d_sweaterstotal + d_scarvestotal;
                    txtTotalAmount.Text = d_total.ToString();

                    //radiobuttons enabled/disabled based from the total amount
                    if (d_total > 50) { radCreditCard.Enabled = true; if (radCreditCard.Checked == true) { grpCreditCardInfo.Enabled = true; } }
                    else { radCreditCard.Enabled = false; grpCreditCardInfo.Enabled = false; }
                    if (d_total > 1500) { radCash.Enabled = false; if (radCash.Checked == true) { radCash.Checked = false; } }
                    else { radCash.Enabled = true; }
                }
                catch 
                {
                    //catches exception
                    txtShoesQuantity.Text = "";
                }
            }
            else
            {
                try
                {
                    if (decimal.Parse(txtShoesQuantity.Text) >= 0)
                    {
                        //deduction from total amount when product item is unticked
                        d_total = d_total - d_shoestotal;
                        txtTotalAmount.Text = d_total.ToString();
                        d_shoestotal = 0;

                        //radiobuttons enabled/disabled based from the total amount
                        if (d_total > 50) { radCreditCard.Enabled = true; if (radCreditCard.Checked == true) { grpCreditCardInfo.Enabled = true; } }
                        else { radCreditCard.Enabled = false; grpCreditCardInfo.Enabled = false; }
                        if (d_total > 1500) { radCash.Enabled = false; if (radCash.Checked == true) { radCash.Checked = false; } }
                        else { radCash.Enabled = true; }
                    }
                    txtShoesQuantity.Enabled = false;
                }
                catch
                {
                    //catches exception
                    txtShoesQuantity.Enabled = false;
                }
            }
        }

        private void txtShoesQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkShoes.Checked)
                {
                    //assignation to variables
                    d_shoesunitprice = decimal.Parse(txtShoesUnitPrice.Text);
                    if (string.IsNullOrEmpty(txtShoesQuantity.Text))
                    { d_shoesquantity = 0; }
                    else { d_shoesquantity = decimal.Parse(txtShoesQuantity.Text); }

                    //initial calculation
                    d_shoestotal = d_shoesunitprice * d_shoesquantity;
                }
                else
                {
                    d_shoestotal = 0;
                }
                //Total Amount calculation
                d_total = d_shoestotal + d_jacketstotal + d_glovestotal + d_beaniestotal + d_sweaterstotal + d_scarvestotal;
                txtTotalAmount.Text = d_total.ToString();

                //radiobuttons enabled/disabled based from the total amount
                if (d_total > 50) { radCreditCard.Enabled = true; if (radCreditCard.Checked == true) { grpCreditCardInfo.Enabled = true; } }
                else { radCreditCard.Enabled = false; grpCreditCardInfo.Enabled = false;}
                if (d_total > 1500) { radCash.Enabled = false; if (radCash.Checked == true) { radCash.Checked = false; } }
                else { radCash.Enabled = true; }
            }
            catch
            {
                //catches exception
                txtShoesQuantity.Text = "";
            }

        }

        private void chkJackets_CheckedChanged(object sender, EventArgs e)
        {//------JACKETS------
            //Statement if Jackets checkbox is ticked
            if (chkJackets.Checked == true)
            {
                txtJacketsQuantity.Enabled = true;

                try
                {
                    if (chkJackets.Checked)
                    {
                        //assignation to variables
                        d_jacketsunitprice = decimal.Parse(txtJacketsUnitPrice.Text);
                        if (string.IsNullOrEmpty(txtJacketsQuantity.Text))
                        { d_jacketsquantity = 0; }
                        else { d_jacketsquantity = decimal.Parse(txtJacketsQuantity.Text); }

                        //initial calculation
                        d_jacketstotal = d_jacketsunitprice * d_jacketsquantity;
                    }
                    else
                    {
                        d_jacketstotal = 0;
                    }
                    //Total Amount calculation
                    d_total = d_shoestotal + d_jacketstotal + d_glovestotal + d_beaniestotal + d_sweaterstotal + d_scarvestotal;
                    txtTotalAmount.Text = d_total.ToString();

                    //radiobuttons enabled/disabled based from the total amount
                    if (d_total > 50) { radCreditCard.Enabled = true; if (radCreditCard.Checked == true) { grpCreditCardInfo.Enabled = true; } }
                    else { radCreditCard.Enabled = false; grpCreditCardInfo.Enabled = false; }
                    if (d_total > 1500) { radCash.Enabled = false; if (radCash.Checked == true) { radCash.Checked = false; } }
                    else { radCash.Enabled = true; }
                }
                catch
                {
                    //catches exception
                    txtJacketsQuantity.Text = "";
                }

            }
            else
            {
                try
                {
                    if (decimal.Parse(txtJacketsQuantity.Text) >= 0)
                    {
                        //deduction from total amount when product item is unticked
                        d_total = d_total - d_jacketstotal;
                        txtTotalAmount.Text = d_total.ToString();
                        d_jacketstotal = 0;

                        //radiobuttons enabled/disabled based from the total amount
                        if (d_total > 50) { radCreditCard.Enabled = true; if (radCreditCard.Checked == true) { grpCreditCardInfo.Enabled = true; } }
                        else { radCreditCard.Enabled = false; grpCreditCardInfo.Enabled = false; }
                        if (d_total > 1500) { radCash.Enabled = false; if (radCash.Checked == true) { radCash.Checked = false; } }
                        else { radCash.Enabled = true; }
                    }
                    txtJacketsQuantity.Enabled = false;
                }
                catch
                {
                    //catches exception
                    txtJacketsQuantity.Enabled = false;
                }
            }
        }

        private void txtJacketsQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkJackets.Checked)
                {
                    //assignation to variables
                    d_jacketsunitprice = decimal.Parse(txtJacketsUnitPrice.Text);
                    if (string.IsNullOrEmpty(txtJacketsQuantity.Text))
                    { d_jacketsquantity = 0; }
                    else { d_jacketsquantity = decimal.Parse(txtJacketsQuantity.Text); }

                    //initial calculation
                    d_jacketstotal = d_jacketsunitprice * d_jacketsquantity;
                }
                else
                {
                    d_jacketstotal = 0;
                }
                //Total Amount calculation
                d_total = d_shoestotal + d_jacketstotal + d_glovestotal + d_beaniestotal + d_sweaterstotal + d_scarvestotal;
                txtTotalAmount.Text = d_total.ToString();

                //radiobuttons enabled/disabled based from the total amount
                if (d_total > 50) { radCreditCard.Enabled = true; if (radCreditCard.Checked == true) { grpCreditCardInfo.Enabled = true; } }
                else { radCreditCard.Enabled = false; grpCreditCardInfo.Enabled = false; }
                if (d_total > 1500) { radCash.Enabled = false; if (radCash.Checked == true) { radCash.Checked = false; } }
                else { radCash.Enabled = true; }
            }
            catch
            {
                //catches exception
                txtJacketsQuantity.Text = "";
            }
        }

        private void chkGloves_CheckedChanged(object sender, EventArgs e)
        {//------GLOVES------
            //Statement if Gloves checkbox is ticked
            if (chkGloves.Checked == true)
            {
                txtGlovesQuantity.Enabled = true;

                try
                {
                    if (chkGloves.Checked)
                    {
                        //assignation to variables
                        d_glovesunitprice = decimal.Parse(txtGlovesUnitPrice.Text);
                        if (string.IsNullOrEmpty(txtGlovesQuantity.Text))
                        { d_glovesquantity = 0; }
                        else { d_glovesquantity = decimal.Parse(txtGlovesQuantity.Text); }
                        //initial calculation
                        d_glovestotal = d_glovesunitprice * d_glovesquantity;
                    }
                    else
                    {
                        d_glovestotal = 0;
                    }
                    //Total Amount calculation
                    d_total = d_shoestotal + d_jacketstotal + d_glovestotal + d_beaniestotal + d_sweaterstotal + d_scarvestotal;
                    txtTotalAmount.Text = d_total.ToString();

                    //radiobuttons enabled/disabled based from the total amount
                    if (d_total > 50) { radCreditCard.Enabled = true; if (radCreditCard.Checked == true) { grpCreditCardInfo.Enabled = true; } }
                    else { radCreditCard.Enabled = false; grpCreditCardInfo.Enabled = false; }
                    if (d_total > 1500) { radCash.Enabled = false; if (radCash.Checked == true) { radCash.Checked = false; } }
                    else { radCash.Enabled = true; }
                }
                catch
                {
                    //catches exception
                    txtGlovesQuantity.Text = "";
                }

            }
            else
            {
                try
                {
                    if (decimal.Parse(txtGlovesQuantity.Text) >= 0)
                    {
                        //deduction from total amount when product item is unticked
                        d_total = d_total - d_glovestotal;
                        txtTotalAmount.Text = d_total.ToString();
                        d_glovestotal = 0;

                        //radiobuttons enabled/disabled based from the total amount
                        if (d_total > 50) { radCreditCard.Enabled = true; if (radCreditCard.Checked == true) { grpCreditCardInfo.Enabled = true; } }
                        else { radCreditCard.Enabled = false; grpCreditCardInfo.Enabled = false; }
                        if (d_total > 1500) { radCash.Enabled = false; if (radCash.Checked == true) { radCash.Checked = false; } }
                        else { radCash.Enabled = true; }
                    }
                    txtGlovesQuantity.Enabled = false;
                }
                catch
                {
                    //catches exception
                    txtGlovesQuantity.Enabled = false;
                }
            }
        }

        private void txtGlovesQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkGloves.Checked)
                {
                    //assignation to variables
                    d_glovesunitprice = decimal.Parse(txtGlovesUnitPrice.Text);
                    if (string.IsNullOrEmpty(txtGlovesQuantity.Text))
                    { d_glovesquantity = 0; }
                    else { d_glovesquantity = decimal.Parse(txtGlovesQuantity.Text); }
                    //initial calculation
                    d_glovestotal = d_glovesunitprice * d_glovesquantity;
                }
                else
                {
                    d_glovestotal = 0;
                }
                //Total Amount calculation
                d_total = d_shoestotal + d_jacketstotal + d_glovestotal + d_beaniestotal +d_sweaterstotal + d_scarvestotal;
                txtTotalAmount.Text = d_total.ToString();

                //radiobuttons enabled/disabled based from the total amount
                if (d_total > 50) { radCreditCard.Enabled = true; if (radCreditCard.Checked == true) { grpCreditCardInfo.Enabled = true; } }
                else { radCreditCard.Enabled = false; grpCreditCardInfo.Enabled = false; }
                if (d_total > 1500) { radCash.Enabled = false; if (radCash.Checked == true) { radCash.Checked = false; } }
                else { radCash.Enabled = true; }
            }
            catch
            {
                //catches exception
                txtGlovesQuantity.Text = "";
            }
        }

        private void chkBeanies_CheckedChanged(object sender, EventArgs e)
        {///------BEANIES------
            //Statement if Beanies checkbox is ticked
            if (chkBeanies.Checked == true)
            {
                txtBeaniesQuantity.Enabled = true;

                try
                {
                    if (chkBeanies.Checked)
                    {
                        //assignation to variables
                        d_beaniesunitprice = decimal.Parse(txtBeaniesUnitPrice.Text);
                        if (string.IsNullOrEmpty(txtBeaniesQuantity.Text))
                        { d_beaniesquantity = 0; }
                        else { d_beaniesquantity = decimal.Parse(txtBeaniesQuantity.Text); }
                        
                        //initial calculation
                        d_beaniestotal = d_beaniesunitprice * d_beaniesquantity;
                    }
                    else
                    {
                        d_beaniestotal = 0;
                    }
                    //Total Amount calculation
                    d_total = d_shoestotal + d_jacketstotal + d_glovestotal + d_beaniestotal + d_sweaterstotal + d_scarvestotal;
                    txtTotalAmount.Text = d_total.ToString();

                    //radiobuttons enabled/disabled based from the total amount
                    if (d_total > 50) { radCreditCard.Enabled = true; if (radCreditCard.Checked == true) { grpCreditCardInfo.Enabled = true; } }
                    else { radCreditCard.Enabled = false; grpCreditCardInfo.Enabled = false; }
                    if (d_total > 1500) { radCash.Enabled = false; if (radCash.Checked == true) { radCash.Checked = false; } }
                    else { radCash.Enabled = true; }
                }
                catch
                {
                    //catches exception
                    txtBeaniesQuantity.Text = "";
                }
            }
            else
            {
                try
                {
                    if (decimal.Parse(txtBeaniesQuantity.Text) >= 0)
                    {
                        //deduction from total amount when product item is unticked
                        d_total = d_total - d_beaniestotal;
                        txtTotalAmount.Text = d_total.ToString();
                        d_beaniestotal = 0;

                        //radiobuttons enabled/disabled based from the total amount
                        if (d_total > 50) { radCreditCard.Enabled = true; if (radCreditCard.Checked == true) { grpCreditCardInfo.Enabled = true; } }
                        else { radCreditCard.Enabled = false; grpCreditCardInfo.Enabled = false; }
                        if (d_total > 1500) { radCash.Enabled = false; if (radCash.Checked == true) { radCash.Checked = false; } }
                        else { radCash.Enabled = true; }
                    }
                    txtBeaniesQuantity.Enabled = false;
                }
                catch
                {
                    //catches exception
                    txtBeaniesQuantity.Enabled = false;
                }
            }
        }

        private void txtBeaniesQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkBeanies.Checked)
                {
                    //assignation to variables
                    d_beaniesunitprice = decimal.Parse(txtBeaniesUnitPrice.Text);
                    if (string.IsNullOrEmpty(txtBeaniesQuantity.Text))
                    { d_beaniesquantity = 0; }
                    else { d_beaniesquantity = decimal.Parse(txtBeaniesQuantity.Text); }

                    //initial calculation
                    d_beaniestotal = d_beaniesunitprice * d_beaniesquantity;
                }
                else
                {
                    d_beaniestotal = 0;
                }
                //Total Amount calculation
                d_total = d_shoestotal + d_jacketstotal + d_glovestotal + d_beaniestotal + d_sweaterstotal + d_scarvestotal;
                txtTotalAmount.Text = d_total.ToString();

                //radiobuttons enabled/disabled based from the total amount
                if (d_total > 50) { radCreditCard.Enabled = true; if (radCreditCard.Checked == true) { grpCreditCardInfo.Enabled = true; } }
                else { radCreditCard.Enabled = false; grpCreditCardInfo.Enabled = false; }
                if (d_total > 1500) { radCash.Enabled = false; if (radCash.Checked == true) { radCash.Checked = false; } }
                else { radCash.Enabled = true; }
            }
            catch
            {
                //catches exception
                txtBeaniesQuantity.Text = "";
            }
        }

        private void chkSweaters_CheckedChanged(object sender, EventArgs e)
        {//------SWEATERS------
            //Statement if Sweaters checkbox is ticked
            if (chkSweaters.Checked == true)
            {
                txtSweatersQuantity.Enabled = true;

                try
                {
                    if (chkSweaters.Checked)
                    {
                        //assignation to variables
                        d_sweatersunitprice = decimal.Parse(txtSweatersUnitPrice.Text);
                        if (string.IsNullOrEmpty(txtSweatersQuantity.Text))
                        { d_sweatersquantity = 0; }
                        else { d_sweatersquantity = decimal.Parse(txtSweatersQuantity.Text); }
                        
                        //initial calculation
                        d_sweaterstotal = d_sweatersunitprice * d_sweatersquantity;
                    }
                    else
                    {
                        d_sweaterstotal = 0;
                    }
                    //Total Amount calculation
                    d_total = d_shoestotal + d_jacketstotal + d_glovestotal + d_beaniestotal + d_sweaterstotal + d_scarvestotal;
                    txtTotalAmount.Text = d_total.ToString();

                    //radiobuttons enabled/disabled based from the total amount
                    if (d_total > 50) { radCreditCard.Enabled = true; if (radCreditCard.Checked == true) { grpCreditCardInfo.Enabled = true; } }
                    else { radCreditCard.Enabled = false; grpCreditCardInfo.Enabled = false; }
                    if (d_total > 1500) { radCash.Enabled = false; if (radCash.Checked == true) { radCash.Checked = false; } }
                    else { radCash.Enabled = true; }
                }
                catch
                {
                    //catches exception
                    txtSweatersQuantity.Text = "";
                }

            }
            else
            {
                try
                {
                    if (decimal.Parse(txtSweatersQuantity.Text) >= 0)
                    {
                        //deduction from total amount when product item is unticked
                        d_total = d_total - d_sweaterstotal;
                        txtTotalAmount.Text = d_total.ToString();
                        d_sweaterstotal = 0;

                        //radiobuttons enabled/disabled based from the total amount
                        if (d_total > 50) { radCreditCard.Enabled = true; if (radCreditCard.Checked == true) { grpCreditCardInfo.Enabled = true; } }
                        else { radCreditCard.Enabled = false; grpCreditCardInfo.Enabled = false; }
                        if (d_total > 1500) { radCash.Enabled = false; if (radCash.Checked == true) { radCash.Checked = false; } }
                        else { radCash.Enabled = true; }
                    }
                    txtSweatersQuantity.Enabled = false;
                }
                catch
                {
                    //catches exception
                    txtSweatersQuantity.Enabled = false;
                }
            }
        }

        private void txtSweatersQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSweaters.Checked)
                {
                    //assignation to variables
                    d_sweatersunitprice = decimal.Parse(txtSweatersUnitPrice.Text);
                    if (string.IsNullOrEmpty(txtSweatersQuantity.Text))
                    { d_sweatersquantity = 0; }
                    else { d_sweatersquantity = decimal.Parse(txtSweatersQuantity.Text); }

                    //initial calculation
                    d_sweaterstotal = d_sweatersunitprice * d_sweatersquantity;
                }
                else
                {
                    d_sweaterstotal = 0;
                }
                //Total Amount calculation
                d_total = d_shoestotal + d_jacketstotal + d_glovestotal + d_beaniestotal + d_sweaterstotal + d_scarvestotal;
                txtTotalAmount.Text = d_total.ToString();

                //radiobuttons enabled/disabled based from the total amount
                if (d_total > 50) { radCreditCard.Enabled = true; if (radCreditCard.Checked == true) { grpCreditCardInfo.Enabled = true; } }
                else { radCreditCard.Enabled = false; grpCreditCardInfo.Enabled = false; }
                if (d_total > 1500) { radCash.Enabled = false; if (radCash.Checked == true) { radCash.Checked = false; } }
                else { radCash.Enabled = true; }
            }
            catch
            {
                //catches exception
                txtSweatersQuantity.Text = "";
            }
        }

        private void chkScarves_CheckedChanged(object sender, EventArgs e)
        {//------SCARVES------
            //Statement if Scarves checkbox is ticked
            if (chkScarves.Checked == true)
            {
                txtScarvesQuantity.Enabled = true;

                try
                {
                    if (chkScarves.Checked)
                    {
                        //assignation to variables
                        d_scarvesunitprice = decimal.Parse(txtScarvesUnitPrice.Text);
                        if (string.IsNullOrEmpty(txtScarvesQuantity.Text))
                        { d_scarvesquantity = 0; }
                        else { d_scarvesquantity = decimal.Parse(txtScarvesQuantity.Text); }

                        //initial calculation
                        d_scarvestotal = d_scarvesunitprice * d_scarvesquantity;
                    }
                    else
                    {
                        d_scarvestotal = 0;
                    }
                    //Total Amount calculation
                    d_total = d_shoestotal + d_jacketstotal + d_glovestotal + d_beaniestotal + d_sweaterstotal + d_scarvestotal;
                    txtTotalAmount.Text = d_total.ToString();

                    //radiobuttons enabled/disabled based from the total amount
                    if (d_total > 50) { radCreditCard.Enabled = true; if (radCreditCard.Checked == true) { grpCreditCardInfo.Enabled = true; } }
                    else { radCreditCard.Enabled = false; grpCreditCardInfo.Enabled = false; }
                    if (d_total > 1500) { radCash.Enabled = false; if (radCash.Checked == true) { radCash.Checked = false; } }
                    else { radCash.Enabled = true; }
                }
                catch
                {
                    //catches exception
                    txtScarvesQuantity.Text = "";
                }

            }
            else
            {
                try
                {
                    if (decimal.Parse(txtScarvesQuantity.Text) >= 0)
                    {
                        //deduction from total amount when product item is unticked
                        d_total = d_total - d_scarvestotal;
                        txtTotalAmount.Text = d_total.ToString();
                        d_scarvestotal = 0;

                        //radiobuttons enabled/disabled based from the total amount
                        if (d_total > 50) { radCreditCard.Enabled = true; if (radCreditCard.Checked == true) { grpCreditCardInfo.Enabled = true; } }
                        else { radCreditCard.Enabled = false; grpCreditCardInfo.Enabled = false; }
                        if (d_total > 1500) { radCash.Enabled = false; if (radCash.Checked == true) { radCash.Checked = false; } }
                        else { radCash.Enabled = true; }
                    }
                    txtScarvesQuantity.Enabled = false;
                }
                catch
                {
                    //catches exception
                    txtScarvesQuantity.Enabled = false;
                }
            }
        }

        private void txtScarvesQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkScarves.Checked)
                {
                    //assignation to variables
                    d_scarvesunitprice = decimal.Parse(txtScarvesUnitPrice.Text);
                    if (string.IsNullOrEmpty(txtScarvesQuantity.Text))
                    { d_scarvesquantity = 0; }
                    else { d_scarvesquantity = decimal.Parse(txtScarvesQuantity.Text); }
                    //initial calculation
                    d_scarvestotal = d_scarvesunitprice * d_scarvesquantity;
                }
                else
                {
                    d_scarvestotal = 0;
                }
                //Total Amount calculation
                d_total = d_shoestotal + d_jacketstotal + d_glovestotal + d_beaniestotal + d_sweaterstotal + d_scarvestotal;
                txtTotalAmount.Text = d_total.ToString();

                //radiobuttons enabled/disabled based from the total amount
                if (d_total > 50) { radCreditCard.Enabled = true; if (radCreditCard.Checked == true) { grpCreditCardInfo.Enabled = true; } }
                else { radCreditCard.Enabled = false; grpCreditCardInfo.Enabled = false; }
                if (d_total > 1500) { radCash.Enabled = false; if (radCash.Checked == true) { radCash.Checked = false; } }
                else { radCash.Enabled = true; }
            }
            catch
            {
                //catches exception
                txtScarvesQuantity.Text = "";
            }
        }

        private void txtCardNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            //------INPUT VERIFICATION------
            //Ensures the input in the textbox are numbers
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }

        private void txtFirstName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //------INPUT VERIFICATION------
            //Ensures the input in the textbox are letters
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void txtLastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //------INPUT VERIFICATION------
            //Ensures the input in the textbox are letters
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }
    }
}

