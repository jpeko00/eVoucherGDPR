using System;
using System.Collections.Generic;
using System.Text;
using eVoucherGDPR.Model;

namespace eVoucherGDPR.Data
{
    class ManualListData
    {
        public List<ManualItem> items = new List<ManualItem>()
        {
             new ManualItem()
            {
                itemID = 1,
                keyWords = "1. Dugme Učitaj QR kod",
                description = "- kliknite za početak skeniranja",
                imgsrc="manual_qr.png"
            },
             new ManualItem()
            {
                itemID = 2,
                keyWords = "2. Pričekajte",
                description = "- potrebno je maksimalno par sekundi dok skener prepozna kod \n- nakon prepoznavanja, prestaje sa skeniranjem",
                imgsrc="manual_load.png"
            },
             new ManualItem()
            {
                itemID = 3,
                keyWords = "3. Unos teksta",
                description = "- Možete unijeti ručno korisnikov alias",
                imgsrc="manual_text.png"
            },
              new ManualItem()
            {
                itemID = 4,
                keyWords = "4. Dugme Dalje", 
                description = "- pritiskom na dugme Dalje pokrećete formu za odabir ili potvrdu iznosa",
                imgsrc="manual_right.png"
            },
               new ManualItem()
            {
                itemID = 5,
                keyWords = "5. Potvrda",
                description = "- potvrdite ili po potrebi izmijenite željeni iznos",
                imgsrc="manual_check.png"
            },
             new ManualItem()
            {
                itemID = 6,
                keyWords = "6. Dugme Slanje",
                description = "- pošaljite podatke web servisu",
                imgsrc="manual_send.png"
            },
             new ManualItem()
            {
                itemID = 7,
                keyWords = "7. Pričekajte potvrdu",
                description = "- ukoliko je transakcija uspješna, dobit ćete potvrdu od web servisa \n- ukoliko nije, dobit ćete obavijest o greški",
                imgsrc="manual_load.png"
            },
             new ManualItem()
            {
                itemID = 8,
                keyWords = "8. Kraj",
                description = "- pri završetku, zatvorite program",
                imgsrc="manual_close.png"
            }
        };

    }
}
