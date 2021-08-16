using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Quickstart.Models.Configuration;
using Quickstart.Models;
using Twilio.TwiML;
using Twilio.TwiML.Voice;
using System.Text.RegularExpressions;

namespace Quickstart.Controllers
{
    public class VoiceController : Controller
    {

        private readonly TwilioAccountDetails _twilioAccountDetails;

        public VoiceController(IOptions<TwilioAccountDetails> twilioAccountDetails)
        {
            _twilioAccountDetails = twilioAccountDetails.Value ?? throw new ArgumentException(nameof(twilioAccountDetails));
        }

        // POST: /voice
        [HttpPost]
        public IActionResult Index(string to, string callingDeviceIdentity)
        {
            var callerId = _twilioAccountDetails.CallerId;

            var twiml = new VoiceResponse();

            Console.WriteLine($"to: {to}, callingDeviceIdentity: {callingDeviceIdentity}, thisDevice.Identity: {Device.Identity}");

            // someone calls into my Twilio Number, there is no thisDeviceIdentity passed to the /voice endpoint 
            if (string.IsNullOrEmpty(callingDeviceIdentity))
            {
                var dial = new Dial();
                var client = new Client();
                client.Identity(Device.Identity);
                dial.Append(client);
                twiml.Append(dial);
            }
            else if (callingDeviceIdentity != Device.Identity)
            {
                var dial = new Dial();
                var client = new Client();
                client.Identity(Device.Identity);
                dial.Append(client);
                twiml.Append(dial);
            }
            // if the POST request contains your browser device's identity
            // make an outgoing call to either another client or a number
            else
            {
                var dial = new Dial(callerId: callerId);

                // check if the 'To' property in the POST request is
                // a client name or a phone number
                // and dial appropriately using either Number or Client

                if (Regex.IsMatch(to, "^[\\d\\+\\-\\(\\) ]+$"))
                {
                    Console.WriteLine("Match is true");
                    dial.Number(to);
                }
                else
                {
                    var client = new Client();
                    client.Identity(to);
                    dial.Append(client);

                }

                twiml.Append(dial);
            }

            Console.WriteLine(twiml.ToString());

            return Content(twiml.ToString(), "text/xml");
        }
    }
}
