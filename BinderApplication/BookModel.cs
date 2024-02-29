using System;
using System.Collections.Generic;

namespace BinderApplication
{
    public class BookModel
    {
        public string? Kind { get; set; }
        public string? Id { get; set; }
        public string? Etag { get; set; }
        public string? SelfLink { get; set; }
        public VolumeInfo? VolumeInfo { get; set; }
        public SaleInfo? SaleInfo { get; set; }
        public AccessInfo? AccessInfo { get; set; }

        public void PrintAllValues()
        {
            Console.WriteLine($"Kind: {Kind}");
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Etag: {Etag}");
            Console.WriteLine($"SelfLink: {SelfLink}");
            VolumeInfo?.PrintAllValues();
            SaleInfo?.PrintAllValues();
            AccessInfo?.PrintAllValues();
        }
    }

    public class VolumeInfo
    {
        public string? Title { get; set; }
        public List<string>? Authors { get; set; }
        public string? Publisher { get; set; }
        public string? PublishedDate { get; set; }
        public string? Description { get; set; }
        public int? PageCount { get; set; }
        public string? PrintType { get; set; }
        public List<string>? Categories { get; set; }
        public double? AverageRating { get; set; }
        public int? RatingsCount { get; set; }
        public string? MaturityRating { get; set; }
        public bool? AllowAnonLogging { get; set; }
        public string? ContentVersion { get; set; }
        public string? Language { get; set; }
        public string? PreviewLink { get; set; }
        public string? InfoLink { get; set; }
        public string? CanonicalVolumeLink { get; set; }
        public ReadingModes? ReadingModes { get; set; }
        public PanelizationSummary? PanelizationSummary { get; set; }
        public ImageLinks? ImageLinks { get; set; }
        public List<IndustryIdentifier>? IndustryIdentifiers { get; set; }

        public void PrintAllValues()
        {
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Authors: {string.Join(", ", Authors ?? new List<string>())}");
            Console.WriteLine($"Publisher: {Publisher}");
            Console.WriteLine($"PublishedDate: {PublishedDate}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"PageCount: {PageCount}");
            Console.WriteLine($"PrintType: {PrintType}");
            Console.WriteLine($"Categories: {string.Join(", ", Categories ?? new List<string>())}");
            Console.WriteLine($"AverageRating: {AverageRating}");
            Console.WriteLine($"RatingsCount: {RatingsCount}");
            Console.WriteLine($"MaturityRating: {MaturityRating}");
            Console.WriteLine($"AllowAnonLogging: {AllowAnonLogging}");
            Console.WriteLine($"ContentVersion: {ContentVersion}");
            Console.WriteLine($"Language: {Language}");
            Console.WriteLine($"PreviewLink: {PreviewLink}");
            Console.WriteLine($"InfoLink: {InfoLink}");
            Console.WriteLine($"CanonicalVolumeLink: {CanonicalVolumeLink}");
            ReadingModes?.PrintAllValues();
            PanelizationSummary?.PrintAllValues();
            ImageLinks?.PrintAllValues();
            IndustryIdentifiers?.ForEach(id => id.PrintAllValues());
        }
    }

    public class ReadingModes
    {
        public bool? Text { get; set; }
        public bool? Image { get; set; }

        public void PrintAllValues()
        {
            Console.WriteLine($"TextReadingMode: {Text}");
            Console.WriteLine($"ImageReadingMode: {Image}");
        }
    }

    public class PanelizationSummary
    {
        public bool? ContainsEpubBubbles { get; set; }
        public bool? ContainsImageBubbles { get; set; }

        public void PrintAllValues()
        {
            Console.WriteLine($"ContainsEpubBubbles: {ContainsEpubBubbles}");
            Console.WriteLine($"ContainsImageBubbles: {ContainsImageBubbles}");
        }
    }

    public class ImageLinks
    {
        public string? SmallThumbnail { get; set; }
        public string? Thumbnail { get; set; }

        public void PrintAllValues()
        {
            Console.WriteLine($"SmallThumbnail: {SmallThumbnail}");
            Console.WriteLine($"Thumbnail: {Thumbnail}");
        }
    }

    public class IndustryIdentifier
    {
        public string? Type { get; set; }
        public string? Identifier { get; set; }

        public void PrintAllValues()
        {
            Console.WriteLine($"IndustryIdentifierType: {Type}");
            Console.WriteLine($"IndustryIdentifier: {Identifier}");
        }
    }

    public class SaleInfo
    {
        public string? Country { get; set; }
        public string? Saleability { get; set; }
        public bool? IsEbook { get; set; }

        public void PrintAllValues()
        {
            Console.WriteLine($"Country: {Country}");
            Console.WriteLine($"Saleability: {Saleability}");
            Console.WriteLine($"IsEbook: {IsEbook}");
        }
    }

    public class AccessInfo
    {
        public string? Country { get; set; }
        public string? Viewability { get; set; }
        public bool? Embeddable { get; set; }
        public bool? PublicDomain { get; set; }
        public string? TextToSpeechPermission { get; set; }
        public Epub? Epub { get; set; }
        public Pdf? Pdf { get; set; }
        public string? WebReaderLink { get; set; }
        public string? AccessViewStatus { get; set; }
        public bool? QuoteSharingAllowed { get; set; }

        public void PrintAllValues()
        {
            Console.WriteLine($"Country: {Country}");
            Console.WriteLine($"Viewability: {Viewability}");
            Console.WriteLine($"Embeddable: {Embeddable}");
            Console.WriteLine($"PublicDomain: {PublicDomain}");
            Console.WriteLine($"TextToSpeechPermission: {TextToSpeechPermission}");
            Epub?.PrintAllValues();
            Pdf?.PrintAllValues();
            Console.WriteLine($"WebReaderLink: {WebReaderLink}");
            Console.WriteLine($"AccessViewStatus: {AccessViewStatus}");
            Console.WriteLine($"QuoteSharingAllowed: {QuoteSharingAllowed}");
        }
    }

    public class Epub
    {
        public bool? IsAvailable { get; set; }

        public void PrintAllValues()
        {
            Console.WriteLine($"EpubIsAvailable: {IsAvailable}");
        }
    }

    public class Pdf
    {
        public bool? IsAvailable { get; set; }

        public void PrintAllValues()
        {
            Console.WriteLine($"PdfIsAvailable: {IsAvailable}");
        }
    }
}
