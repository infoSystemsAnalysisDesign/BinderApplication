using Microsoft.Maui.Controls;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BinderApplication.Pages
{
    public partial class Match : ContentPage
    {
        public Match()
        {
            InitializeComponent();
            LoadData();
        }

        /*
         * This is currently only going to handle 1 book per API call as we test it,
         * it will later be turned into a function that can support X amount of books from
         * an API call
        */

        private async void LoadData()
        {
            try
            {

                using (HttpClient httpClient = new HttpClient())
                {
                    string apiUrl = "https://www.googleapis.com/books/v1/volumes?q=subject:fiction&maxResults=1";

                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        //responseLabel.Text = responseBody;
                        parseAPIOutput(responseBody);
                    }
                    else
                    {
                        responseLabel.Text = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                    }
                }
            }
            catch (Exception ex)
            {
                responseLabel.Text = $"Exception: {ex.Message}";
            }
        }

        private void parseAPIOutput(string stringToBeParsed)
        {
            writeToFile(stringToBeParsed);  //sends initial un-parsed output to a file named OFILE in your documents folder (for testing)
            responseLabel.Text = stringToBeParsed;  //will write unparsed data to screen (temp for testing)

            //This is dumb code that is dumb. It will be changed so that Book is a class and each book is an object of that class, but this
            //is for testing.
            string bookID, bookEtag, bookLink, title, author, publisher, publishedDate, description, ISBN_13, ISBN, pageCount, printType, category,
                averageRating, webReaderLink;



            //What is outputted and must be parsed
            /*
             {
  "kind": "books#volumes",
  "totalItems": 200,
  "items": [
    {
      "kind": "books#volume",
      "id": "KVGd-NabpW0C",
      "etag": "HgYzEVnBQdw",
      "selfLink": "https://www.googleapis.com/books/v1/volumes/KVGd-NabpW0C",
      "volumeInfo": {
        "title": "The Plague",
        "authors": [
          "Albert Camus"
        ],
        "publisher": "Vintage",
        "publishedDate": "1991-05-07",
        "description": "“Its relevance lashes you across the face.” —Stephen Metcalf, The Los Angeles Times • “A redemptive book, one that wills the reader to believe, even in a time of despair.” —Roger Lowenstein, The Washington Post A haunting tale of human resilience and hope in the face of unrelieved horror, Albert Camus' iconic novel about an epidemic ravaging the people of a North African coastal town is a classic of twentieth-century literature. The townspeople of Oran are in the grip of a deadly plague, which condemns its victims to a swift and horrifying death. Fear, isolation and claustrophobia follow as they are forced into quarantine. Each person responds in their own way to the lethal disease: some resign themselves to fate, some seek blame, and a few, like Dr. Rieux, resist the terror. An immediate triumph when it was published in 1947, The Plague is in part an allegory of France's suffering under the Nazi occupation, and a timeless story of bravery and determination against the precariousness of human existence.",
        "industryIdentifiers": [
          {
            "type": "ISBN_13",
            "identifier": "9780679720218"
          },
          {
            "type": "ISBN_10",
            "identifier": "0679720219"
          }
        ],
        "readingModes": {
          "text": false,
          "image": false
        },
        "pageCount": 312,
        "printType": "BOOK",
        "categories": [
          "Fiction"
        ],
        "averageRating": 4.5,
        "ratingsCount": 14,
        "maturityRating": "NOT_MATURE",
        "allowAnonLogging": false,
        "contentVersion": "1.1.1.0.preview.0",
        "panelizationSummary": {
          "containsEpubBubbles": false,
          "containsImageBubbles": false
        },
        "imageLinks": {
          "smallThumbnail": "http://books.google.com/books/content?id=KVGd-NabpW0C&printsec=frontcover&img=1&zoom=5&edge=curl&source=gbs_api",
          "thumbnail": "http://books.google.com/books/content?id=KVGd-NabpW0C&printsec=frontcover&img=1&zoom=1&edge=curl&source=gbs_api"
        },
        "language": "en",
        "previewLink": "http://books.google.com/books?id=KVGd-NabpW0C&printsec=frontcover&dq=subject:fiction&hl=&cd=1&source=gbs_api",
        "infoLink": "http://books.google.com/books?id=KVGd-NabpW0C&dq=subject:fiction&hl=&source=gbs_api",
        "canonicalVolumeLink": "https://books.google.com/books/about/The_Plague.html?hl=&id=KVGd-NabpW0C"
      },
      "saleInfo": {
        "country": "US",
        "saleability": "NOT_FOR_SALE",
        "isEbook": false
      },
      "accessInfo": {
        "country": "US",
        "viewability": "PARTIAL",
        "embeddable": true,
        "publicDomain": false,
        "textToSpeechPermission": "ALLOWED",
        "epub": {
          "isAvailable": false
        },
        "pdf": {
          "isAvailable": false
        },
        "webReaderLink": "http://play.google.com/books/reader?id=KVGd-NabpW0C&hl=&source=gbs_api",
        "accessViewStatus": "SAMPLE",
        "quoteSharingAllowed": false
      }
    }
  ]
}

*/
        }

        private static void writeToFile(string textToBeWrittenToFile)
        {
            // Create a string array with the lines of text
            string[] lines = { textToBeWrittenToFile };

            // Set a variable to the Documents path.
            string docPath =
              Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "OFILE.txt")))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
        }

    }
}

