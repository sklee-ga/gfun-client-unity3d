using System;
using System.Collections;
using LitJson;

namespace GFSDK
{
	public class GFResult
	{
		public static string KEY_CODE = "code";
		public static string KEY_MSG = "msg";
		public static string KEY_REGID = "gameRegId";
		
		int response;
		string message;
		string regId;
		
		public GFResult( int response, string message )
		{
			this.response = response;
			this.message = message;
		}
		
		public GFResult( string json )
		{
            try
            {
				Console.WriteLine("====----=====1");
                JsonData jsonData = JsonMapper.ToObject(json);
                this.response = Convert.ToInt32(jsonData[KEY_CODE].ToString());
				Console.WriteLine("====----=====2");
                this.message = jsonData[KEY_MSG].ToString();
				Console.WriteLine("====----=====3");
				this.regId = jsonData[KEY_REGID].ToString();

				Console.WriteLine("====----=====4");
				Console.WriteLine(jsonData.ToString());
				Console.WriteLine(json);
				
//				Console.WriteLine("result : code > {0} , msg > {1}", this.response, this.message);
            }
            catch (Exception e)
            {
				this.response = ResultCode.EXCEPTION_OCCURE;
				this.message = "Exception occured";
                Console.WriteLine(e.ToString());
            }
		}
		
		public int getResponse()
		{
			return response;
		}
		
	    public string getMessage()
		{
			return message;
		}

		public string getRegId() {
			return regId;
		}
		
	    public bool isSuccess()
		{
			return ( response == ResultCode.OK );
		}

        public string toJSONString()
        {
            LitJson.JsonWriter writer = new LitJson.JsonWriter();                
            writer.WriteObjectStart();
            writer.WritePropertyName("code"); writer.Write(response);
            writer.WritePropertyName("msg"); writer.Write(message);
            writer.WriteObjectEnd();

            return writer.ToString();
        }
	}

	public class ResultCode
	{
		public static int OK 						= 0;

		public static int INITIALIZE_FAIL           = 1;

		public static int PARAMETER_INVALIDATE      = 2;

		public static int EXCEPTION_OCCURE			= 3;

//		public static int MARKET_ERROR              = 3;
//
//		public static int NO_SUPPORTED_API          = 4;
//
//		public static int NO_SUPPORTED_GOOGLEPLAY   = 8;
//
//		public static int ITEM_INVALIDATE           = 8;
//
//		public static int PURCHASE_INCOMPLETE       = 9;
	}
}

