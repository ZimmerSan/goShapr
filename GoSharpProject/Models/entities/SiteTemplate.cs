using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoSharpProject.Models.constants;

namespace GoSharpProject.Models.entities
{
    public class SiteTemplate
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id {get; set;}
        [Required(ErrorMessage = "An Item Name is required")]
        [StringLength(160)]
        public string Name {get; set;}
        [Display(Name = "Short description")]
        public string ShortDescription {get; set;}
        [Display(Name = "Product description")]
        [DataType(DataType.MultilineText)]
        public string Description {get; set;}
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be between 0.01 and 999999.99")]
        public decimal Price { get; set;}

        public override string ToString()
        {
            return Name + " " + Price + " " + Description;
        }

        public byte[] InternalImage { get; set; }

        [Display(Name = "Image File")]
        [NotMapped]
        public HttpPostedFileBase File
        {
            get
            {
                return null;
            }

            set
            {
                try
                {
                    MemoryStream target = new MemoryStream();

                    if (value.InputStream == null)
                        return;

                    value.InputStream.CopyTo(target);
                    InternalImage = target.ToArray();
                }
                catch (Exception ex)
                {
                    //logger.Error(ex.Message);
                    // logger.Error(ex.StackTrace);
                }
            }
        }

        [DisplayName("Item Picture URL")]
        [StringLength(1024)]
        public string ItemPictureUrl { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }

        public TemplateSiteTypes Category { get; set; }

    }

}