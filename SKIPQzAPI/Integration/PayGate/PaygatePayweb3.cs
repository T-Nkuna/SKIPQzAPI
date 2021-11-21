/*
 * Copyright (c) 2021 PayGate (Pty) Ltd
 *
 * Author: App Inlet (Pty) Ltd
 * 
 * Released under the GNU General Public License
 */

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web;
using System.Text;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Specialized;

/// <summary>
/// Class that provides initiate and query functionality for PayGate PayWeb3 applications
/// </summary>
public class PaygatePayweb3
{
    /**
     * The url of the PayGate PayWeb 3 initiate page
     */
    static public string initiate_url = "https://secure.paygate.co.za/payweb3/initiate.trans";

    /**
	 * The url of the PayGate PayWeb 3 process page
	 */
    static public string process_url = "https://secure.paygate.co.za/payweb3/process.trans";

    /**
	 * The url of the PayGate PayWeb 3 query page
	 */
    static public string query_url = "https://secure.paygate.co.za/payweb3/query.trans";

    /**
	 * Dictionary contains the data returned from the initiate, required for the redirect of the client
	 */
    public Dictionary<string, string> processRequest = new Dictionary<string, string>();


    /**
	 * String
	 *
	 * Most common errors returned will be:
	 *
	 * DATA_CHK    -> Checksum posted does not match the one calculated by PayGate, either due to an incorrect encryption key used or a field that has been excluded from the checksum calculation
	 * DATA_PW     -> Mandatory fields have been excluded from the post to PayGate, refer to page 9 of the documentation as to what fields should be posted.
	 * DATA_CUR    -> The currency that has been posted to PayGate is not supported.
	 * PGID_NOT_EN -> The PayGate ID being used to post data to PayGate has not yet been enabled, or there are no payment methods setup on it.
	 *
	 */
    public string lastError;

    private Dictionary<int, string> transactionStatusArray = new Dictionary<int, string>()
    {
        {1, "Approved" },
        {2, "Declined" },
        {4, "Cancelled" }
    };

    public Dictionary<string, string> QueryResponse { get; } = new Dictionary<string, string>();
   
    public bool IsSSl { get; set; } = false;

    public Dictionary<string,string> RequestPayload { get; set; }

    public string EncryptionKey { get; set; }


    /**
	 * Returns a description of the transaction status number passed back from PayGate
	 *
	 * @param int statusNumber
	 * @return string
	 */
    public string getTransactionStatusDescription(int statusNumber)
    {
        return this.transactionStatusArray[statusNumber];
    }

    /**
	 * Function to generate the checksum to be passed in the initiate call. Refer to examples on Page 19 of the PayWeb3 documentation (Version 1.03.2)
	 *
	 * @param Dictionary<string, string> postData
	 * @return string (md5 hash value)
	 */
    public string GenerateChecksum(Dictionary<string, string> postData)
    {
        string checksum = "";

        foreach (var item in postData)
        {
            if (item.Value != "")
            {
                checksum += item.Value;
            }
        }

        checksum += this.EncryptionKey;

        var hash = MD5.Create().ComputeHash(System.Text.Encoding.ASCII.GetBytes(checksum));
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("x2"));
        }

        return sb.ToString();
    }

    /**
     * Function to compare checksums 
     * 
     * @param Dictionary<string, string> data
     * @return bool
    */
    public bool ValidateChecksum(Dictionary<string, string> data)
    {
        string returnedChecksum = data["CHECKSUM"];
        data.Remove("CHECKSUM");

        string checksum = this.GenerateChecksum(data);
        return String.Compare(returnedChecksum, checksum) == 0;
    }

   

    /**
	 * Function to handle response from Query request and set error as need be
	 *
	 * @return bool
	 */
    public bool HandleQueryResponse()
    {
        if (this.QueryResponse.ContainsKey("ERROR"))
        {
            this.lastError = this.QueryResponse["ERROR"];
            this.QueryResponse.Clear();
            return false;
        }
        return true;
    }

    /**
	 * Function to do post to PayGate to initiate a PayWeb 3 transaction
	 *
	 * @return bool
	 */
    public async Task<NameValueCollection> DoInitiate()
    {

        this.RequestPayload["CHECKSUM"] = this.GenerateChecksum(this.RequestPayload);

        var response =  await this.DoCurlPost(this.RequestPayload, initiate_url);
        var type = response.GetType();

        if (type.Name == "String")
        {
            var returnedResult = HttpUtility.ParseQueryString(response);
            return returnedResult;
        }

        return null;
    }

    /**
	 * Function to do  post to PayGate to query a PayWeb 3 transaction
	 * 
	 * @return bool
	 */
    public async  Task<bool> DoQuery()
    {
        this.RequestPayload["CHECKSUM"] = this.GenerateChecksum(this.RequestPayload);

        var result =  await this.DoCurlPost(this.RequestPayload, query_url);
        var type = result.GetType();
        var isSuccess = false;

        if (type.Name == "String")
        {
            this.QueryResponse.Clear();
            var response = HttpUtility.ParseQueryString(result);
            foreach (var key in response.AllKeys)
            {
                this.QueryResponse[key] = response.Get(key);
            }
            isSuccess = this.HandleQueryResponse();
        }

        return isSuccess;
    }


    /**
    * function to do actual post to PayGate
    *
    * @param Dictionary postData - data to be posted
    * @param string url - Url to be posted to
    * @return bool | string
    */
    public async Task<string> DoCurlPost(Dictionary<string,string> payload,string transactUrl)
    {
        var client = new HttpClient();
        var content = new FormUrlEncodedContent(payload);

        client.BaseAddress = new Uri(transactUrl);

        //Check to see if is a secure connection
        if (!this.IsSSl)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        var response = await client.PostAsync(transactUrl, content);
        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync(); 
            return responseString;
        }

        return string.Empty;
    }
}