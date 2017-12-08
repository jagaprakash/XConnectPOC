using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.WebApi;
using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.Schema;
using Sitecore.Xdb.Common.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySample.Xconnect
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("");
            Console.WriteLine("END OF PROGRAM.");
            Console.ReadKey();
        }

        private static async Task MainAsync(string[] args)
        {
            CertificateWebRequestHandlerModifierOptions options =
            CertificateWebRequestHandlerModifierOptions.Parse("StoreName=My;StoreLocation=LocalMachine;FindType=FindByThumbprint;FindValue=‎06F22FCCFD4CCC202A3300F3F35EBA732CBD6802");

            var certificateModifier = new CertificateWebRequestHandlerModifier(options);

            List<IHttpClientModifier> clientModifiers = new List<IHttpClientModifier>();
            var timeoutClientModifier = new TimeoutHttpClientModifier(new TimeSpan(0, 0, 20));
            clientModifiers.Add(timeoutClientModifier);

            var collectionClient = new CollectionWebApiClient(new Uri("https://ScCoveo.xconnect/odata"), clientModifiers, new[] { certificateModifier });
            var searchClient = new SearchWebApiClient(new Uri("https://ScCoveo.xconnect/odata"), clientModifiers, new[] { certificateModifier });
            var configurationClient = new ConfigurationWebApiClient(new Uri("https://ScCoveo.xconnect/configuration"), clientModifiers, new[] { certificateModifier });

            var cfg = new XConnectClientConfiguration(
                new XdbRuntimeModel(CollectionModel.Model), collectionClient, searchClient, configurationClient);

            try
            {
                await cfg.InitializeAsync();

                // Print xConnect if configuration is valid
                var arr = new[]
                {
                        @"            ______                                                       __     ",
                        @"           /      \                                                     |  \    ",
                        @" __    __ |  $$$$$$\  ______   _______   _______    ______    _______  _| $$_   ",
                        @"|  \  /  \| $$   \$$ /      \ |       \ |       \  /      \  /       \|   $$ \  ",
                        @"\$$\/  $$| $$      |  $$$$$$\| $$$$$$$\| $$$$$$$\|  $$$$$$\|  $$$$$$$ \$$$$$$   ",
                        @" >$$  $$ | $$   __ | $$  | $$| $$  | $$| $$  | $$| $$    $$| $$        | $$ __  ",
                        @" /  $$$$\ | $$__/  \| $$__/ $$| $$  | $$| $$  | $$| $$$$$$$$| $$_____   | $$|  \",
                        @"|  $$ \$$\ \$$    $$ \$$    $$| $$  | $$| $$  | $$ \$$     \ \$$     \   \$$  $$",
                        @" \$$   \$$  \$$$$$$   \$$$$$$  \$$   \$$ \$$   \$$  \$$$$$$$  \$$$$$$$    \$$$$ "
                    };
                Console.WindowWidth = 160;
                foreach (string line in arr)
                    Console.WriteLine(line);

            }
            catch (XdbModelConflictException ce)
            {
                Console.WriteLine("ERROR:" + ce.Message);
                return;
            }

            #region SetContact
            /////initialize a client using the validated configuration
            //using (var client = new XConnectClient(cfg))
            //{
            //    try
            //    {


            //        // Identifier for a 'known' contact
            //        var identifier = new ContactIdentifier[]
            //        {
            //                        new ContactIdentifier("twitter", "myrtlesitecore" + Guid.NewGuid().ToString("N"), ContactIdentifierType.Known)
            //        };

            //        // Print out the identifier that is going to be used
            //        Console.WriteLine("Identifier:" + identifier[0].Identifier);

            //        // Create a new contact with the identifier
            //        Contact knownContact = new Contact(identifier);

            //        PersonalInformation personalInfoFacet = new PersonalInformation();

            //        personalInfoFacet.FirstName = "Ganaesan";
            //        personalInfoFacet.LastName = "NM";
            //        personalInfoFacet.JobTitle = "Programmer Developer";

            //        client.SetFacet<PersonalInformation>(knownContact, PersonalInformation.DefaultFacetKey, personalInfoFacet);

            //        client.AddContact(knownContact);


            //        var offlineGoal = Guid.Parse("ad8ab7fe-ab48-4ea9-a976-ae7a268ae2f0"); // "Watched demo" goal
            //        var channelId = Guid.Parse("110cbf07-6b1a-4743-a398-6749acfcd7aa"); // "Other event" channel


            //        // Create a new interaction for that contact
            //        Interaction interaction = new Interaction(knownContact, InteractionInitiator.Brand, channelId, "");

            //        // Add events - all interactions must have at least one event
            //        var xConnectEvent = new Goal(offlineGoal, DateTime.UtcNow);
            //        interaction.Events.Add(xConnectEvent);

            //        IpInfo ipInfo = new IpInfo("163.0.0.1");

            //        ipInfo.BusinessName = "FromGanaesan";

            //        client.SetFacet<IpInfo>(interaction, IpInfo.DefaultFacetKey, ipInfo);

            //        // Add the contact and interaction
            //        client.AddInteraction(interaction);

            //        // Submit contact and interaction - a total of two operations
            //        await client.SubmitAsync();

            //        // Get the last batch that was executed
            //        var operations = client.LastBatch;

            //        Console.WriteLine("RESULTS...");

            //        // Loop through operations and check status
            //        foreach (var operation in operations)
            //        {
            //            Console.WriteLine(operation.OperationType + operation.Target.GetType().ToString() + " Operation: " + operation.Status);
            //        }

            //        Console.ReadLine();
            //    }
            //    catch (XdbExecutionException ex)
            //    {
            //        // Deal with exception
            //    }
            //}

            #endregion SetContact

            //using (var client = new XConnectClient(cfg))
            //{
            //    try
            //    {
            //        // Get a known contact
            //        IdentifiedContactReference reference = new IdentifiedContactReference("twitter", "myrtlesitecorededc0479307647e0b2566f02ab05ed36");

            //        Contact existingContact = await client.GetAsync<Contact>(reference, new ContactExpandOptions(new string[] { PersonalInformation.DefaultFacetKey })
            //        {
            //            Interactions = new RelatedInteractionsExpandOptions(IpInfo.DefaultFacetKey)
            //            {
            //                StartDateTime = DateTime.MinValue,
            //                EndDateTime = DateTime.MaxValue
            //            }
            //        });

            //        PersonalInformation existingContactFacet = existingContact.GetFacet<PersonalInformation>(PersonalInformation.DefaultFacetKey);

            //        Console.WriteLine("Contact ID: " + existingContact.Id.ToString());
            //        Console.WriteLine("Contact Name: " + existingContactFacet.FirstName);
            //        Console.WriteLine("Interaction Count:" + existingContact.Interactions.Count);
            //        //Console.WriteLine("Intereaction BusinessName:" + existingContact);

            //        Console.ReadLine();
            //    }
            //    catch (XdbExecutionException ex)
            //    {
            //        // Deal with exception
            //    }
            //}

            using (var client = new XConnectClient(cfg))
            {
                try
                {
                    var results0 = client.Contacts.ToEnumerable().Count();

                    Console.WriteLine("Total contacts: " + results0.ToString());

                    // Use InteractionsCache instead of client.Contacts.Where(x => x.Interactions.Any()) as not all search providers support joins
                    //var results = await client.Contacts.Where(c => c.InteractionsCache().InteractionCaches.Any()).GetBatchEnumerator();
                    var results = await client.Contacts.Where(c => c.InteractionsCache().InteractionCaches.Any()).WithExpandOptions(new ContactExpandOptions(PersonalInformation.DefaultFacetKey)
                    {
                        Interactions = new RelatedInteractionsExpandOptions(IpInfo.DefaultFacetKey)
                        {
                            EndDateTime = DateTime.MaxValue,
                            StartDateTime = DateTime.MinValue
                        }
                    })
                   .GetBatchEnumerator();

                    Console.WriteLine("Contacts with interactions: " + results.TotalCount);

                    var results2 = await client.Contacts.Where(c => c.LastModified > DateTime.UtcNow.AddHours(-10)).GetBatchEnumerator();

                    Console.WriteLine("Updated 10hrs ago: " + results2.TotalCount);

                    var results3 = await client.Contacts.Where(c => c.GetFacet<PersonalInformation>().JobTitle == "Programmer Writer").GetBatchEnumerator();

                    Console.WriteLine("Programmer Writers: " + results3.TotalCount);

                    //var results4 = await client.Interactions.Where(i => i.EndDateTime > DateTime.UtcNow.AddHours(-10)).GetBatchEnumerator();
                    var results4 = await client.Interactions.Where(i => i.EndDateTime > DateTime.UtcNow.AddHours(-10))
                            .WithExpandOptions(new InteractionExpandOptions(new string[] { IpInfo.DefaultFacetKey })
                            {
                                Contact = new RelatedContactExpandOptions(PersonalInformation.DefaultFacetKey)
                            })
                            .GetBatchEnumerator();
                    Console.WriteLine("Interactions < 10hrs old: " + results4.TotalCount);

                    Console.ReadKey();
                }
                catch (XdbExecutionException ex)
                {
                    // Deal with exception
                }
            }


        }
    }
}