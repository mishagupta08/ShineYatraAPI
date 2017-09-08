using System.Configuration;

namespace ShineYatraApi
{
    public class RechargeTemplate
    {
        /// <summary>
        /// Hold value of recharge call
        /// </summary>
        public string PrepaidRechargeValues = "AuthorisationCode=5998799662498&product=productValue&MobileNumber=MobileNumberValue&Amount=AmountValue&RequestId=RequestIdValue";

        /// <summary>
        /// Hold value of postpaid recharge call value
        /// </summary>
        public string PostpaidRechargeValues = "AuthorisationCode=AuthorisationCodeValue&product=productValue&MobileNumber=MobileNumberValue&Amount=AmountValue&RequestId=RequestIdValue&Circle=CircleValue&AcountNo=AcountNoValue&StdCode=StdCodeValue";

        /// <summary>
        /// Holds Services values
        /// </summary>
        public string ServiceValues = "https://www.instantpay.in/ws/api/serviceproviders?token=63a3c58a814a9fe4e684246673b72027&type=typeValue&format=json";

        /// <summary>
        /// transaction validation request json
        /// </summary>
        public string TransactionValidationJson = "https://www.instantpay.in/ws/api/transaction?token=63a3c58a814a9fe4e684246673b72027&mode=VALIDATE&spkey=spkeyValue&agentid=agentidValue&account=accountValue&amount=amountValue&optional1=AircelRechargeTest&format=json";

        /// <summary>
        /// transaction request json
        /// </summary>
        public string TransactionJson = "https://www.instantpay.in/ws/api/transaction?token=63a3c58a814a9fe4e684246673b72027&spkey=spkeyValue&agentid=agentidValue&account=accountValue&amount=amountValue&optional1=AircelRechargeTest&format=json";

        /// <summary>
        /// transaction status request json
        /// </summary>
        public string StatusJson = "https://www.instantpay.in/ws/api/getMIS?token=63a3c58a814a9fe4e684246673b72027&agentid=agentidValue&format=json";

        /// <summary>
        /// call back values
        /// </summary>
        public string CallbackJson = "?ipay_id=ipay_idValue&agent_id=agent_idValue&opr_id=opr_idValue&status=statusValue&res_code=res_codeValue&res_msg=res_msgValue";

        //public string checkTrans = "https://www.instantpay.in/ws/api/getMIS?token=63a3c58a814a9fe4e684246673b72027&agentid=agentidValue&format=json";
    }
}