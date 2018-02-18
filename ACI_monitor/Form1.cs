using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;

using RestSharp;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using SnmpSharpNet;

namespace ACI_monitor
{
    public partial class Main : Form
    {
        string apicIP = ConfigurationManager.AppSettings.Get("APIC-1_IP");
        string apic_id = ConfigurationManager.AppSettings.Get("APIC_ID");
        string apic_pw = ConfigurationManager.AppSettings.Get("APIC_PASS");
        string line_token = ConfigurationManager.AppSettings.Get("Line_token");

        string operState_101_eth12;
        string operState_102_eth12;
        string adminState_101_eth12;
        string adminState_102_eth12;    

        int log_101_count = 0;
        int log_102_count = 0;
        int log_id_101_number = 1;
        int log_id_102_number = 1;

        int auto_shutdown_count = 0 , MAX_Shutdown = 1;
        int auto_shutdown_101_state = 0;
        int auto_shutdown_102_state = 0;

        RestClient client = new RestClient();

        public Main()
        {
            InitializeComponent();
            client.Timeout = 2000;
            /* 
            if(Login_API()==true)
            {
                Get_first_log();
            }
            */
            //LineNotify("TEST");
        }

        private Boolean Login_API(string username , string password)
        {
            string sessionId = "", payload = "";
            RestRequest login_post;
            IRestResponse login_post_response;
            JObject login_data;

            client.BaseUrl = new System.Uri("https://" + apicIP + "/api/aaaLogin.json");
            client.CookieContainer = new System.Net.CookieContainer();
            ServicePointManager.ServerCertificateValidationCallback += (RestClient, certificate, chain, sslPolicyErrors) => true;

            payload = "payload{\"aaaUser\":{\"attributes\":{\"name\":\"" + username + "\", \"pwd\":\"" + password + "\"}}}";

            login_post = new RestRequest(Method.POST);
            login_post.AddHeader("content-type", "application/json");
            login_post.AddParameter("application/json", payload, ParameterType.RequestBody);

            try
            {
                login_post_response = client.Execute(login_post);
                login_data = JObject.Parse(login_post_response.Content);
                sessionId = (login_data["imdata"][0]["aaaLogin"]["attributes"]["sessionId"].ToString());

                Status_Connect.Text = "sessionId = " + sessionId;

                Interface_101_clock.Enabled = true;
                Interface_102_clock.Enabled = true;
                Log_Tick_101.Enabled = true;
                Log_Tick_102.Enabled = true;
                aaaRefresh.Enabled = true;

            }
            catch
            {
                Status_Connect.Text = "Can't connect to " + apicIP;
                LineNotify("Can't connect to " + apicIP );
                MessageBox.Show("Can't connect to " + apicIP , "Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true; // no error

        }

        private bool Get_first_log()
        {
            log_101_count = 0;
            log_102_count = 0;

            JObject datastat;
            string descr_filed101, code_filed101;

            //POD-1 NODE-101 PORT-ETH1/2
            RestRequest request_101 = new RestRequest();
            client.BaseUrl = new System.Uri("https://" + apicIP + "/api/node/mo/topology/pod-1/node-101/sys/phys-[eth1/2].json?rsp-subtree-include=event-logs,no-scoped,subtree&order-by=eventRecord.created|desc");
            ServicePointManager.ServerCertificateValidationCallback += (RestRequest, certificate, chain, sslPolicyErrors) => true;

            request_101 = new RestRequest(Method.GET);
            request_101.AddHeader("cache-control", "no-cache");

            try
            {
                IRestResponse response101 = client.Execute(request_101);
                datastat = JObject.Parse(response101.Content);
                log_101_count = int.Parse((datastat["totalCount"].ToString()));
                LogBox_101.Text = log_101_count + "\r\n";
                for (int i = log_101_count - 1 ; i >= 0; i--)
                {
                    code_filed101 = (datastat["imdata"][i]["eventRecord"]["attributes"]["code"].ToString());
                    descr_filed101 = (datastat["imdata"][i]["eventRecord"]["attributes"]["descr"].ToString());

                    LogBox_101.Text += "ID " + log_id_101_number++ + " Code " + code_filed101 + " " + descr_filed101 + "\r\n";
                }
                log_101_count++;
            }
            catch
            {
                Status_Connect.Text = "Can't connect to " + apicIP;
                LineNotify("Can't connect to " + apicIP);
                MessageBox.Show("Can't connect to " + apicIP, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //===================================================================================================================

            //POD-1 NODE-102 PORT-ETH1/2
            RestRequest request_102 = new RestRequest(Method.GET);
            client.BaseUrl = new System.Uri("https://" + apicIP + "/api/node/mo/topology/pod-1/node-102/sys/phys-[eth1/2].json?rsp-subtree-include=event-logs,no-scoped,subtree&order-by=eventRecord.created|desc");
            ServicePointManager.ServerCertificateValidationCallback += (RestRequest, certificate, chain, sslPolicyErrors) => true;

            request_102 = new RestRequest(Method.GET);
            request_102.AddHeader("cache-control", "no-cache");

            try
            {
                IRestResponse response102 = client.Execute(request_102);
                datastat = JObject.Parse(response102.Content);
                log_102_count = int.Parse((datastat["totalCount"].ToString()));
                LogBox_102.Text = log_102_count + "\r\n";
                string descr_filed102, code_filed102;
                for (int i = log_102_count - 1 ; i >= 0; i--)
                {
                    code_filed102 = (datastat["imdata"][i]["eventRecord"]["attributes"]["code"].ToString());
                    descr_filed102 = (datastat["imdata"][i]["eventRecord"]["attributes"]["descr"].ToString());
                    
                    LogBox_102.Text += "ID " + log_id_102_number++ + " Code " + code_filed102 + " " + descr_filed102 + "\r\n";
                }
                log_102_count++;
            }
            catch
            {
                Status_Connect.Text = "Can't connect to " + apicIP;
                LineNotify("Can't connect to " + apicIP);
                MessageBox.Show("Can't connect to " + apicIP, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //===================================================================================================================

            return true; // no error
        }

        private int LineNotify(string msg)
        {
            //RestRequest line_post;
            //RestClient line_client = new RestClient();

            //line_client.BaseUrl = new System.Uri("https://notify-api.line.me/api/notify");
            //ServicePointManager.ServerCertificateValidationCallback += (RestClient, certificate, chain, sslPolicyErrors) => true;

            //line_post = new RestRequest(Method.POST); ;
            //line_post.AddHeader("Authorization", string.Format("Bearer " + line_token));
            //line_post.AddHeader("content-type", "application/x-www-form-urlencoded");
            //line_post.AddParameter("message", msg);

            //try
            //{
            //    IRestResponse response = line_client.Execute(line_post);
            //    return int.Parse((JObject.Parse(response.Content)["status"].ToString()));
            //}
            //catch
            //{
            //    Status_Connect.Text = "Can't connect to line server !!";
            //    MessageBox.Show("Can't connect to line server !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            //}
        }

        private void Interface_101_clock_Tick(object sender, EventArgs e)
        {
            RestRequest request1;
            IRestResponse response1;
            JObject datastat;
            string urlmoniter;

            string pod = "pod-1";
            string node = "101";
            string port = "eth1/2";
            urlmoniter = "https://" + apicIP + "/api/node/mo/topology/" + pod + "/node-" + node + "/sys/phys-[" + port + "].json";
            client.BaseUrl = new System.Uri(urlmoniter);
            ServicePointManager.ServerCertificateValidationCallback += (RestRequest, certificate, chain, sslPolicyErrors) => true;
            request1 = new RestRequest(Method.GET);
            request1.AddHeader("cache-control", "no-cache");
            try
            {
                response1 = client.Execute(request1);
                datastat = JObject.Parse(response1.Content);
                adminState_101_eth12 = (datastat["imdata"][0]["l1PhysIf"]["attributes"]["adminSt"].ToString());
            }
            catch
            {
                adminState_101_eth12_led.BackColor = Color.Yellow;
                LineNotify("Can't Get status Please re login");
                MessageBox.Show("Can't Get status Please re login");
                Interface_101_clock.Enabled = false;
                return;
            }

            urlmoniter = "https://" + apicIP + "/api/node/mo/topology/" + pod + "/node-" + node + "/sys/phys-[" + port + "].json?query-target=children&target-subtree-class=ethpmPhysIf&rsp-subtree=no";
            client.BaseUrl = new System.Uri(urlmoniter);
            ServicePointManager.ServerCertificateValidationCallback += (RestRequest, certificate, chain, sslPolicyErrors) => true;
            request1 = new RestRequest(Method.GET);
            request1.AddHeader("cache-control", "no-cache");
            try
            {
                response1 = client.Execute(request1);
                datastat = JObject.Parse(response1.Content);
                operState_101_eth12 = (datastat["imdata"][0]["ethpmPhysIf"]["attributes"]["operSt"].ToString());
            }
            catch
            {
                operState_101_eth12_led.BackColor = Color.Yellow;
                LineNotify("Can't Get status Please re login");
                MessageBox.Show("Can't Get status Please re login");
                Interface_101_clock.Enabled = false;
                return;
            }

            /*
            textBox1.Text = "";
            textBox1.Text += pod + " " + node + " " + port + "\r\n";
            textBox1.Text += "adminState " + adminState_101_eth12 + "\r\n";
            textBox1.Text += "operState " + operState_101_eth12 + "\r\n";
            */

            // =============== POD1 NODE 101 ETH1/1 ===============
            if (adminState_101_eth12 == "up")
            {
                adminState_101_eth12_led.BackColor = Color.Green;
                Shutdown_101_ETH12.Enabled = true;
                Enable_101_ETH12.Enabled = false;
            }
            else
            {
                adminState_101_eth12_led.BackColor = Color.Red;
                Shutdown_101_ETH12.Enabled = false;
                Enable_101_ETH12.Enabled = true;
            }
            if (operState_101_eth12 == "up")
            {
                operState_101_eth12_led.BackColor = Color.Green;
            }
            else
            {
                operState_101_eth12_led.BackColor = Color.Red;
            }
            // ====================================================
        }

        private void Interface_102_clock_Tick(object sender, EventArgs e)
        {
            RestRequest request1;
            IRestResponse response1;
            JObject datastat;
            string urlmoniter;

            string pod = "pod-1";
            string node = "102";
            string port = "eth1/2";
            urlmoniter = "https://" + apicIP + "/api/node/mo/topology/" + pod + "/node-" + node + "/sys/phys-[" + port + "].json";
            client.BaseUrl = new System.Uri(urlmoniter);
            ServicePointManager.ServerCertificateValidationCallback += (RestRequest, certificate, chain, sslPolicyErrors) => true;

            request1 = new RestRequest(Method.GET);
            request1.AddHeader("cache-control", "no-cache");

            try
            {
                response1 = client.Execute(request1);
                datastat = JObject.Parse(response1.Content);
                adminState_102_eth12 = (datastat["imdata"][0]["l1PhysIf"]["attributes"]["adminSt"].ToString());
            }
            catch
            {
                adminState_102_eth12_led.BackColor = Color.Yellow;
                LineNotify("Can't Get status Please re login");
                MessageBox.Show("Can't Get status Please re login");
                Interface_102_clock.Enabled = false;
                return;
            }

            urlmoniter = "https://" + apicIP + "/api/node/mo/topology/" + pod + "/node-" + node + "/sys/phys-[" + port + "].json?query-target=children&target-subtree-class=ethpmPhysIf&rsp-subtree=no";
            client.BaseUrl = new System.Uri(urlmoniter);
            ServicePointManager.ServerCertificateValidationCallback += (RestRequest, certificate, chain, sslPolicyErrors) => true;

            request1 = new RestRequest(Method.GET);
            request1.AddHeader("cache-control", "no-cache");

            try
            {
                response1 = client.Execute(request1);
                datastat = JObject.Parse(response1.Content);
                operState_102_eth12 = (datastat["imdata"][0]["ethpmPhysIf"]["attributes"]["operSt"].ToString());
            }
            catch
            {
                operState_102_eth12_led.BackColor = Color.Yellow;
                LineNotify("Can't Get status Please re login");
                MessageBox.Show("Can't Get status Please re login");
                Interface_102_clock.Enabled = false;
                return;
            }

            /*
            textBox1.Text += "\r\n";
            textBox1.Text += pod + " " + node + " " + port + "\r\n";
            textBox1.Text += "adminState " + adminState_102_eth12 + "\r\n";
            textBox1.Text += "operState " + operState_102_eth12 + "\r\n";
            */

            // =============== POD1 NODE 102 ETH1/1 ===============
            if (adminState_102_eth12 == "up")
            {
                adminState_102_eth12_led.BackColor = Color.Green;
                Shutdown_102_ETH12.Enabled = true;
                Enable_102_ETH12.Enabled = false;
            }
            else
            {
                adminState_102_eth12_led.BackColor = Color.Red;
                Shutdown_102_ETH12.Enabled = false;
                Enable_102_ETH12.Enabled = true;
            }
            if (operState_102_eth12 == "up")
            {
                operState_102_eth12_led.BackColor = Color.Green;
            }
            else
            {
                operState_102_eth12_led.BackColor = Color.Red;
            }
            // ====================================================
        }

        private void Log_101_Tick(object sender, EventArgs e)
        {
            var request = new RestRequest(Method.GET);

            client.BaseUrl = new System.Uri("https://" + apicIP + "/api/node/mo/topology/pod-1/node-101/sys/phys-[eth1/2].json?rsp-subtree-include=event-logs,no-scoped,subtree&order-by=eventRecord.created|desc&page=0&page-size=25");
            request = new RestRequest(Method.GET);
            ServicePointManager.ServerCertificateValidationCallback += (RestRequest, certificate, chain, sslPolicyErrors) => true;
            request.AddHeader("cache-control", "no-cache");
            IRestResponse response1 = client.Execute(request);

            try
            {
                JObject datastat = JObject.Parse(response1.Content);
                int log_check = int.Parse((datastat["totalCount"].ToString()));

                string descr_filed, code_filed, changeSet_filed, affected ;
                
                for (int i = log_check - log_101_count ; i >= 0; i--)
                {
                    code_filed = (datastat["imdata"][i]["eventRecord"]["attributes"]["code"].ToString());
                    descr_filed = (datastat["imdata"][i]["eventRecord"]["attributes"]["descr"].ToString());
                    changeSet_filed = (datastat["imdata"][i]["eventRecord"]["attributes"]["changeSet"].ToString());
                    bool check_word = changeSet_filed.Contains("link-failure");

                    if ( (code_filed == "E4205126" && check_word) )
                    {
                        affected = (datastat["imdata"][i]["eventRecord"]["attributes"]["affected"].ToString());
                        LineNotify(affected + " " + descr_filed);
                        if (auto_shutdown_count < MAX_Shutdown)
                        {
                            auto_shutdown_count++;
                            auto_shutdown_101_state = 1;
                            Shutdown_101_12();
                        }
                        else
                        {
                            LineNotify("MAXIMUM AUTO SHUTDOWN");
                        }
                    }
                    if (code_filed == "E4205125")
                    {
                        affected = (datastat["imdata"][i]["eventRecord"]["attributes"]["affected"].ToString());
                        LineNotify(affected + " " + descr_filed);
                        if (auto_shutdown_101_state == 1)
                        {
                            auto_shutdown_101_state = 0;
                            auto_shutdown_count--;
                        }
                    }
                    LogBox_101.Text += "ID " + log_id_101_number++ + " Code " + code_filed + " " + descr_filed + "\r\n";
                }
                log_101_count = log_check+1;
            }
            catch
            {
                Status_Connect.Text = "Can't connect apic log";
                LineNotify("Can't connect apic log");
                MessageBox.Show("Can't connect apic log");
                Log_Tick_101.Enabled = false;
            }

        }

        private void Log_102_Tick(object sender, EventArgs e)
        {
            var request = new RestRequest(Method.GET);

            client.BaseUrl = new System.Uri("https://" + apicIP + "/api/node/mo/topology/pod-1/node-102/sys/phys-[eth1/2].json?rsp-subtree-include=event-logs,no-scoped,subtree&order-by=eventRecord.created|desc&page=0&page-size=25");

            request = new RestRequest(Method.GET);
            ServicePointManager.ServerCertificateValidationCallback += (RestRequest, certificate, chain, sslPolicyErrors) => true;
            request.AddHeader("cache-control", "no-cache");
            IRestResponse response1 = client.Execute(request);

            try
            {
                JObject datastat = JObject.Parse(response1.Content);
                int log_check = int.Parse((datastat["totalCount"].ToString()));

                string descr_filed, code_filed, affected , changeSet_filed;

                for (int i = log_check - log_102_count ; i >= 0; i--)
                {
                    code_filed = (datastat["imdata"][i]["eventRecord"]["attributes"]["code"].ToString());
                    descr_filed = (datastat["imdata"][i]["eventRecord"]["attributes"]["descr"].ToString());
                    changeSet_filed = (datastat["imdata"][i]["eventRecord"]["attributes"]["changeSet"].ToString());
                    bool check_word = changeSet_filed.Contains("link-failure");

                    if ( (code_filed == "E4205126" && check_word) )
                    {
                        affected = (datastat["imdata"][i]["eventRecord"]["attributes"]["affected"].ToString());
                        LineNotify(affected + " " + descr_filed);
                        if (auto_shutdown_count < MAX_Shutdown)
                        {
                            auto_shutdown_count++;
                            auto_shutdown_102_state = 1;
                            Shutdown_102_12();
                        }
                        else
                        {
                            LineNotify("MAXIMUM AUTO SHUTDOWN");
                        }
                    }
                    if(code_filed == "E4205125")
                    {
                        affected = (datastat["imdata"][i]["eventRecord"]["attributes"]["affected"].ToString());
                        LineNotify(affected + " " + descr_filed);
                        if (auto_shutdown_102_state == 1)
                        {
                            auto_shutdown_102_state = 0;
                            auto_shutdown_count--;
                        }
                    }
                    LogBox_102.Text += "ID " + log_id_102_number++ + " Code " + code_filed + " " + descr_filed + "\r\n";
                }
                log_102_count = log_check+1;
            }
            catch
            {
                Status_Connect.Text = "Can't connect apic log";
                LineNotify("Can't connect apic log");
                MessageBox.Show("Can't connect apic log");
                Log_Tick_102.Enabled = false;
            }
        }

        private void AaaRefresh_Tick(object sender, EventArgs e)
        {
            /*
            var request = new RestRequest(Method.GET);

            client.BaseUrl = new System.Uri("https://" + apicIP + "/api/aaaRefresh.json");
            client.CookieContainer = new System.Net.CookieContainer();
            request = new RestRequest(Method.GET);
            ServicePointManager.ServerCertificateValidationCallback += (RestRequest, certificate, chain, sslPolicyErrors) => true;
            request.AddHeader("cache-control", "no-cache");
            IRestResponse response1 = client.Execute(request);

            try
            {
                JObject datastat = JObject.Parse(response1.Content);
            }
            catch
            {
                Status_Connect.Text = "Can't connect to " + apicIP;
                Log_Tick_101.Enabled = false;
                Log_Tick_102.Enabled = false;
                Interface_101_clock.Enabled = false;
                Interface_102_clock.Enabled = false;
                aaaRefresh.Enabled = false;
            }
            */
            Login_API(apic_id, apic_pw);
        }

        private void Shutdown_101_12()
        {
            RestRequest request;
            IRestResponse response;
            string Input;

            //var confirmResult = MessageBox.Show("Are you sure to shutdown POD-1 Node-101 Eth1/2 ??", "Confirm Shutdown!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            client.BaseUrl = new System.Uri("https://" + apicIP + "/api/node/mo/uni/fabric/outofsvc.json");
            ServicePointManager.ServerCertificateValidationCallback += (RestClient, certificate, chain, sslPolicyErrors) => true;
            Input = "payload{\"fabricRsOosPath\":{\"attributes\":{\"tDn\":\"topology/pod-1/paths-101/pathep-[eth1/2]\",\"lc\":\"blacklist\"},\"children\":[]}}";

            request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", Input, ParameterType.RequestBody);

            try
            {
                response = client.Execute(request);
                LineNotify("shutdown POD-1 Node-101 Eth1/2");
            }
            catch
            {
                MessageBox.Show("Error to run command!");
            }
        }

        private void Shutdown_102_12()
        {
            RestRequest request;
            IRestResponse response;
            string Input;

            client.BaseUrl = new System.Uri("https://" + apicIP + "/api/node/mo/uni/fabric/outofsvc.json");
            ServicePointManager.ServerCertificateValidationCallback += (RestClient, certificate, chain, sslPolicyErrors) => true;
            Input = "payload{\"fabricRsOosPath\":{\"attributes\":{\"tDn\":\"topology/pod-1/paths-102/pathep-[eth1/2]\",\"lc\":\"blacklist\"},\"children\":[]}}";

            request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", Input, ParameterType.RequestBody);

            try
            {
                response = client.Execute(request);
                LineNotify("shutdown POD-1 Node-102 Eth1/2");
            }
            catch
            {
                MessageBox.Show("Error to run command!");
            }

        }

        private void Shutdown_101_ETH12_Click(object sender, EventArgs e)
        {
            RestRequest request;
            IRestResponse response;
            string Input;

            var confirmResult = MessageBox.Show("Are you sure to shutdown POD-1 Node-101 Eth1/2 ??","Confirm Shutdown!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                client.BaseUrl = new System.Uri("https://" + apicIP + "/api/node/mo/uni/fabric/outofsvc.json");
                ServicePointManager.ServerCertificateValidationCallback += (RestClient, certificate, chain, sslPolicyErrors) => true;
                Input = "payload{\"fabricRsOosPath\":{\"attributes\":{\"tDn\":\"topology/pod-1/paths-101/pathep-[eth1/2]\",\"lc\":\"blacklist\"},\"children\":[]}}";

                request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", Input, ParameterType.RequestBody);

                try
                {
                    response = client.Execute(request);
                    MessageBox.Show("run command!");
                }
                catch
                {
                    MessageBox.Show("Error to run command!");
                }
            }
            else
                return;
        }

        private void Shutdown_102_ETH12_Click(object sender, EventArgs e)
        {
            RestRequest request;
            IRestResponse response;
            string Input;

            var confirmResult = MessageBox.Show("Are you sure to shutdown POD-1 Node-102 Eth1/2 ??", "Confirm Shutdown!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                client.BaseUrl = new System.Uri("https://" + apicIP + "/api/node/mo/uni/fabric/outofsvc.json");
                ServicePointManager.ServerCertificateValidationCallback += (RestClient, certificate, chain, sslPolicyErrors) => true;
                Input = "payload{\"fabricRsOosPath\":{\"attributes\":{\"tDn\":\"topology/pod-1/paths-102/pathep-[eth1/2]\",\"lc\":\"blacklist\"},\"children\":[]}}";

                request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", Input, ParameterType.RequestBody);

                try
                {
                    response = client.Execute(request);
                    MessageBox.Show("run command!");
                }
                catch
                {
                    MessageBox.Show("Error to run command!");
                }
            }
            else
                return;
        }

        private void Enable_101_ETH12_Click(object sender, EventArgs e)
        {
            RestRequest request;
            IRestResponse response;
            string Input;

            var confirmResult = MessageBox.Show("Are you sure to enable POD-1 Node-102 Eth1/2 ??", "Confirm Enable!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                client.BaseUrl = new System.Uri("https://" + apicIP + "/api/node/mo/uni/fabric/outofsvc.json");
                ServicePointManager.ServerCertificateValidationCallback += (RestClient, certificate, chain, sslPolicyErrors) => true;
                Input = "payload{\"fabricRsOosPath\":{\"attributes\":{\"dn\":\"uni/fabric/outofsvc/rsoosPath-[topology/pod-1/paths-101/pathep-[eth1/2]]\",\"status\":\"deleted\"},\"children\":[]}}";

                request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", Input, ParameterType.RequestBody);

                try
                {
                    response = client.Execute(request);
                    MessageBox.Show("run command!");
                    Int_Norun_101.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Error to run command!");
                }
            }
            else
                return;
        }

        private void Enable_102_ETH12_Click(object sender, EventArgs e)
        {
            RestRequest request;
            IRestResponse response;
            string Input;

            var confirmResult = MessageBox.Show("Are you sure to enable POD-1 Node-102 Eth1/2 ??", "Confirm Enable!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                client.BaseUrl = new System.Uri("https://" + apicIP + "/api/node/mo/uni/fabric/outofsvc.json");
                ServicePointManager.ServerCertificateValidationCallback += (RestClient, certificate, chain, sslPolicyErrors) => true;
                Input = "payload{\"fabricRsOosPath\":{\"attributes\":{\"dn\":\"uni/fabric/outofsvc/rsoosPath-[topology/pod-1/paths-102/pathep-[eth1/2]]\",\"status\":\"deleted\"},\"children\":[]}}";

                request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", Input, ParameterType.RequestBody);

                try
                {
                    response = client.Execute(request);
                    MessageBox.Show("run command!");
                    Int_Norun_102.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Error to run command!");
                }
            }
            else
                return;
        }

        private void LogBox_101_TextChanged(object sender, EventArgs e)
        {
            LogBox_101.SelectionStart = LogBox_101.Text.Length;
            LogBox_101.ScrollToCaret();
        }

        private void LogBox_102_TextChanged(object sender, EventArgs e)
        {
            LogBox_102.SelectionStart = LogBox_102.Text.Length;
            LogBox_102.ScrollToCaret();
        }

        private void LoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Login_API(apic_id, apic_pw) == true)
            {
                LogBox_101.Text = "";
                LogBox_102.Text = "";
                Get_first_log();
            }
        }

        private void Int_Norun_101_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            RestRequest request1;
            IRestResponse response1;
            JObject datastat;
            string urlmoniter;

            string pod = "pod-1";
            string node = "101";
            string port = "eth1/2";
            string OperState ;

            urlmoniter = "https://" + apicIP + "/api/node/mo/topology/" + pod + "/node-" + node + "/sys/phys-[" + port + "].json?query-target=children&target-subtree-class=ethpmPhysIf&rsp-subtree=no";
            client.BaseUrl = new System.Uri(urlmoniter);
            ServicePointManager.ServerCertificateValidationCallback += (RestRequest, certificate, chain, sslPolicyErrors) => true;

            request1 = new RestRequest(Method.GET);
            request1.AddHeader("cache-control", "no-cache");

            try
            {
                response1 = client.Execute(request1);
                datastat = JObject.Parse(response1.Content);
                OperState = (datastat["imdata"][0]["ethpmPhysIf"]["attributes"]["operSt"].ToString());

                if(OperState == "down")
                {
                    Shutdown_101_12();
                    LineNotify("Interface 101 1/2 not connect, shutdown!");
                }

            }
            catch
            {
                MessageBox.Show("Can't Get");
            }

            Int_Norun_101.Enabled = false;
        }

        private void Int_Norun_102_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            RestRequest request1;
            IRestResponse response1;
            JObject datastat;
            string urlmoniter;

            string pod = "pod-1";
            string node = "102";
            string port = "eth1/2";
            string OperState;

            urlmoniter = "https://" + apicIP + "/api/node/mo/topology/" + pod + "/node-" + node + "/sys/phys-[" + port + "].json?query-target=children&target-subtree-class=ethpmPhysIf&rsp-subtree=no";
            client.BaseUrl = new System.Uri(urlmoniter);
            ServicePointManager.ServerCertificateValidationCallback += (RestRequest, certificate, chain, sslPolicyErrors) => true;

            request1 = new RestRequest(Method.GET);
            request1.AddHeader("cache-control", "no-cache");

            try
            {
                response1 = client.Execute(request1);
                datastat = JObject.Parse(response1.Content);
                OperState = (datastat["imdata"][0]["ethpmPhysIf"]["attributes"]["operSt"].ToString());

                if (OperState == "down")
                {
                    Shutdown_102_12();
                    LineNotify("Interface 102 1/2 not connect, shutdown!");
                }

            }
            catch
            {
                MessageBox.Show("Can't Get");
            }

            Int_Norun_102.Enabled = false;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
