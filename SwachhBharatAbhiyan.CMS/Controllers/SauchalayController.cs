using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace SwachhBharatAbhiyan.CMS.Controllers
{
    public class SauchalayController : Controller
    {
        IChildRepository childRepository;
        IMainRepository mainRepository;


        public SauchalayController()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                mainRepository = new MainRepository();
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
            }
            else
                Redirect("/Account/Login");
        }

        public ActionResult SauchalayIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult SauchalayMenuIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult SauchalayRegistrationIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult SauchalayRegistrationMenuIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult AddSauchalayDetails(int teamId = -2)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                SauchalayDetailsVM details = childRepository.GetSauchalayById(teamId);
                
                return View(details);

            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult AddSauchalayDetailsOld(SauchalayDetailsVM data, HttpPostedFileBase filesUpload, HttpPostedFileBase filesUpload1)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);

                var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);

                string path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;

                if (filesUpload != null)
                {
                    var guid = Guid.NewGuid().ToString().Split('-');
                    string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

                    string imagePath = Path.Combine(Server.MapPath("/Images/CTPTImages/Images"), image_Guid);
                    var exists = System.IO.Directory.Exists(Server.MapPath("Images/CTPTImages/Images"));

                    if (!exists)
                    {
                        // System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + "/GameImages"));
                        System.IO.Directory.CreateDirectory(Server.MapPath("/Images/CTPTImages/Images"));
                    }
                    filesUpload.SaveAs(imagePath);
                    //Info.Image = AppDetails.baseImageUrl + AppDetails.basePath + "/GameImages/" + image_Guid;
                    data.Image = baseUrl + "/Images/CTPTImages/Images/" + image_Guid;
                }

                if (filesUpload1 != null)
                {
                    var guid = Guid.NewGuid().ToString().Split('-');
                    string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

                    string imagePath = Path.Combine(Server.MapPath("/Images/CTPTImages/Qr"), image_Guid);
                    var exists = System.IO.Directory.Exists(Server.MapPath("/Images/CTPTImages/Qr"));

                    if (!exists)
                    {
                        // System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + "/GameImages"));
                        System.IO.Directory.CreateDirectory(Server.MapPath("//Images/CTPTImages/Qr"));
                    }
                    filesUpload1.SaveAs(imagePath);
                    //Info.Image = AppDetails.baseImageUrl + AppDetails.basePath + "/GameImages/" + image_Guid;
                    data.QrImage = baseUrl + "/Images/CTPTImages/Qr/" + image_Guid;
                }

                int teamId = data.Id;
                var houseId = childRepository.GetSauchalayById(teamId);

                SauchalayDetailsVM sauchalayDetails = childRepository.SaveSauchalay(data);
                return Redirect("SauchalayRegistrationIndex");
            }
            else
                return Redirect("/Account/Login");
        }


        [HttpPost]
        public ActionResult AddSauchalayDetails(SauchalayDetailsVM data)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);

                var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);

                string path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;

                int teamId = data.Id;
                var ctptId = childRepository.GetSauchalayById(teamId);

                if (ctptId.SauchalayQRCode == "/Images/QRcode.png" || ctptId.SauchalayQRCode == "/Images/default_not_upload.png")
                {
                    ctptId.SauchalayQRCode = null;
                }
                if (ctptId.SauchalayQRCode == null)
                {
                    var guid = Guid.NewGuid().ToString().Split('-');
                    string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

                    //Converting  Url to image 
                    // var url = string.Format("http://api.qrserver.com/v1/create-qr-code/?data="+ house.ReferanceId);
                    var url = string.Format("https://chart.googleapis.com/chart?cht=qr&chl=" + data.ReferanceId + "&chs=160x160&chld=L|0");
                    WebResponse response = default(WebResponse);
                    Stream remoteStream = default(Stream);
                    StreamReader readStream = default(StreamReader);
                    WebRequest request = WebRequest.Create(url);
                    response = request.GetResponse();
                    remoteStream = response.GetResponseStream();
                    readStream = new StreamReader(remoteStream);
                    //Creating Path to save image in folder
                    System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
                    string imgpath = Path.Combine(Server.MapPath(AppDetails.basePath + AppDetails.CTPTQRCode), image_Guid);
                    var exists = System.IO.Directory.Exists(Server.MapPath(AppDetails.basePath + AppDetails.CTPTQRCode));
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + AppDetails.CTPTQRCode));
                    }
                    img.Save(imgpath);
                    response.Close();
                    remoteStream.Close();
                    readStream.Close();
                    data.SauchalayQRCode = image_Guid;
                }
                else
                {

                    string bb = ctptId.SauchalayQRCode;
                    var ii = bb.Split('/');
                    if (ii.Length == 6)
                    {
                        data.SauchalayQRCode = ii[ii.Length - 1];
                    }
                    if (ii.Length > 6)
                    {
                        data.SauchalayQRCode = ii[ii.Length - 1];
                    }
                }


                SauchalayDetailsVM sauchalayDetails = childRepository.SaveSauchalay(data);
                return Redirect("SauchalayRegistrationIndex");
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MenuCtptMap()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult CtptMap()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                ViewBag.lat = SessionHandler.Current.Latitude;
                ViewBag.lang = SessionHandler.Current.Logitude;

                List<SauchalayDetailsVM> obj = new List<SauchalayDetailsVM>();
                obj = childRepository.GetCTPTLocation();
                return View(obj);
            }
            else
                return Redirect("/Account/Login");
        }


        public ActionResult Export(int id)
        {
            if (SessionHandler.Current.AppId != 0)
            {

                var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
                string Filename = "", owner = "";

                var details = childRepository.GetSauchalayById(id);
                string cdatetime = DateTime.Now.ToString("_ddmmyyyyhhmmss");
                if (!string.IsNullOrEmpty(details.Name))
                {
                    Filename = Regex.Replace(details.Name, @"\s+", "") + cdatetime + ".pdf";
                    //owner = details.houseOwner;
                }
                else if (!string.IsNullOrEmpty(details.SauchalayID))
                {
                    Filename = Regex.Replace(details.SauchalayID, @"\s+", "") + cdatetime + ".pdf";
                    //owner = details.houseOwner;
                }
                else if(!string.IsNullOrEmpty(details.ReferanceId))
                {
                    Filename = Regex.Replace(details.ReferanceId.ToString(), @"\s+", "") + cdatetime + ".pdf";
                    //owner = "_ _ _ _ _ _ _ _ _ _ _ _";

                }
                else
                {
                    Filename = Regex.Replace(details.Id.ToString(), @"\s+", "") + cdatetime + ".pdf";
                    //owner = "_ _ _ _ _ _ _ _ _ _ _ _";

                }
                string GridHtml = "";
                //string src = AppDetails.baseImageUrlCMS + "/Content/images/img/app_icon_cms.png";
                //string GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #fff;border : 2px solid black;'><div style='text-align:center;margin-top: 8px;font-size:22px;background: #abd037;'> O </div> <div style='background: #abd037;;font-weight: bold;font-size: 18px;'> " + AppDetails.AppName + "</div><div style='font-size: 15px;background: #abd037;'> House Id: " + details.ReferanceId + " </div><div style='height:10px;background: #abd037;'></div> <div style='height:10px;background: #fff;'></div><div style='background: #fff;'> <img style='width:250px;height:250px;' src='" + details.houseQRCode + "'/> </div></div>";


                if (SessionHandler.Current.AppId == 3068) // For Nagpur ULB
                {
                    string src = AppDetails.baseImageUrlCMS + "/Content/images/icons/Nagpur_logo.png";
                    //For Satana Only
                    GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #fff;border : 2px solid black;'><div style='text-align:center;padding-top: 5px;background: #abd037;'><img style='width:250px;height:86px;' src='" + src + "'/></div><div style='font-size: 14px;background: #abd037;'><b> CTPT Id: " + details.ReferanceId + "</b></div><div style='height:10px;background: #fff;'></div><div style='background: #fff;'><img style='width:245px;height:245px;' src='" + details.SauchalayQRCode + "'/></div></div>";
                }
                else
                {
                    //string slogan = AppDetails.baseImageUrlCMS + "/Content/images/icons/slogan.png";
                    //string topimg = AppDetails.baseImageUrlCMS + "/content/images/icons/top_outdoor.png";
                    //string leftbottomimg = AppDetails.baseImageUrlCMS + "/Content/images/icons/left_outdoor.png";
                    //string dry_wet_new = AppDetails.baseImageUrlCMS + "/Content/images/icons/Khapa/dry&wet.png";

                    string top_img_new = AppDetails.baseImageUrlCMS + AppDetails.basePath + "Content/icons/Top_image.png";
                    string slogan_new = AppDetails.baseImageUrlCMS + AppDetails.basePath + "Content/icons/slogan.png";
                    string round = AppDetails.baseImageUrlCMS + AppDetails.basePath + "Content/icons/round.png";

                    //string top_img_new = "http://localhost:34557/" + AppDetails.basePath + "Content/icons/Top_image.png";
                    //string slogan_new = "http://localhost:34557/" + AppDetails.basePath + "Content/icons/slogan.png";
                    //string round = "http://localhost:34557/" + AppDetails.basePath + "Content/icons/round.png";

                    //GridHtml = "<div style='width:100%;height: 100%;background:#ffffff;border : 2px solid #4fa30a;'><div style='float:left;width:7%;padding-top:110px;padding-left:8px;'><img src='" + round + "' style = 'width:20px;height:20px;margin-left:5px;'/></div><div style='float:left;width:58%;padding-left:16px;padding-top:7px;'><img src='" + details.SauchalayQRCode + "' style = 'width:20px;height:20px;'/></div><div style='float:left;width:83%;padding-left:5px;padding-top:10px;padding-bottom:6px;'><div style='padding-left:5px;'><img style='width:150px;height:95px;' src='" + top_img_new + "'/></div><div style='text-align: center;font-weight: 900;padding-bottom:3px;'>&nbsp;&nbsp;&nbsp;<span style='color:#000000;text-align: center;font-size: 16px'>CTPT Id</span><br/><span style='color:#000000;text-align: center;font-size: 21px'>" + details.ReferanceId + "</span></div><div style='padding-left:5px;'><img src='" + slogan_new + "' style='width: 150px; height:49px;'/><br/><div style='float:right;'><b style='font-size:9px;'>" + details.SerielNo + "</b></div></div></div><div style='float:left;width:3%;padding-top:110px;padding-left:22px;text-align:center;'><img src='" + round + "' style = 'width:20px;height:20px;'/></div></div>";
                    GridHtml = "<div style='width:100%;height: 100%;background:#ffffff;border : 2px solid #4fa30a;'><div style='float:left;width:7%;padding-top:110px;padding-left:8px;'><img src='" + round + "' style = 'width:20px;height:20px;margin-left:5px;'/></div><div style='float:left;width:58%;padding-left:16px;padding-top:7px;'><img src='" + details.SauchalayQRCode + "' style = 'width:20px;height:20px;'/></div><div style='float:left;width:83%;padding-left:5px;padding-top:10px;padding-bottom:6px;'><div style='padding-left:5px;'><img style='width:150px;height:95px;' src='" + top_img_new + "'/></div><div style='text-align: center;font-weight: 900;padding-bottom:3px;'><span style='color:#000000;text-align: center;font-size: 16px'>CTPT Id</span><br/><span style='color:#000000;text-align: center;font-size: 15px'>" + details.ReferanceId + "</span></div><div style='padding-left:5px;'><img src='" + slogan_new + "' style='width: 150px; height:49px;'/><br/><div style='float:right;'><b style='font-size:9px;'>" + details.SerielNo + "</b></div></div></div><div style='float:left;width:3%;padding-top:110px;padding-left:22px;text-align:center;'><img src='" + round + "' style = 'width:20px;height:20px;'/></div></div>";

                }


                using (MemoryStream stream = new System.IO.MemoryStream())
                {
                    StringReader sr = new StringReader(GridHtml);

                    if (SessionHandler.Current.AppId == 3068)
                    {
                        var pgSize = new iTextSharp.text.Rectangle(216, 288);
                        Document pdfDoc = new Document(pgSize, 1f, 1f, 1f, 1f);
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();
                        var content = writer.DirectContent;
                        var pageBorderRect = new Rectangle(pdfDoc.PageSize);

                        pageBorderRect.Left += pdfDoc.LeftMargin;
                        pageBorderRect.Right -= pdfDoc.RightMargin;
                        pageBorderRect.Top -= pdfDoc.TopMargin;
                        pageBorderRect.Bottom += pdfDoc.BottomMargin;

                        content.SetColorStroke(BaseColor.BLACK);
                        content.SetLineWidth(5);
                        content.Rectangle(pageBorderRect.Left, pageBorderRect.Bottom, pageBorderRect.Width, pageBorderRect.Height);
                        content.Stroke();
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                        pdfDoc.Close();
                    }
                    else
                    {
                        var pgSize = new iTextSharp.text.Rectangle(324, 180);
                        Document pdfDoc = new Document(pgSize, 1f, 1f, 1f, 1f);
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();
                        var content = writer.DirectContent;
                        var pageBorderRect = new Rectangle(pdfDoc.PageSize);

                        pageBorderRect.Left += pdfDoc.LeftMargin;
                        pageBorderRect.Right -= pdfDoc.RightMargin;
                        pageBorderRect.Top -= pdfDoc.TopMargin;
                        pageBorderRect.Bottom += pdfDoc.BottomMargin;

                        content.SetColorStroke(BaseColor.BLACK);
                        content.SetLineWidth(5);
                        content.Rectangle(pageBorderRect.Left, pageBorderRect.Bottom, pageBorderRect.Width, pageBorderRect.Height);
                        content.Stroke();
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                        pdfDoc.Close();
                    }

                    return File(stream.ToArray(), "application/pdf", Filename);
                }
            }
            else
                return Redirect("/Account/Login");
        }


        public ActionResult GenratePDF(string name,string SauchalayID, string ReferanceId, int Id)
        {
            if (SessionHandler.Current.AppId != 0 && name != null && ReferanceId != null)
            {

                var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
                
                string Filename = "", owner = "";


                string cdatetime = DateTime.Now.ToString("_ddmmyyyyhhmmss");

                if (!string.IsNullOrEmpty(name))
                {
                    Filename = name + "(" + cdatetime + ").pdf";
                }
                else if (!string.IsNullOrEmpty(SauchalayID))
                {
                    Filename = SauchalayID + "(" + cdatetime + ").pdf";

                }
                else if(!string.IsNullOrEmpty(ReferanceId))
                {
                    Filename = ReferanceId + "(" + cdatetime + ").pdf";
                    
                }
                else
                {
                    Filename = Id + "(" + cdatetime + ").pdf";

                }
                //if (name != null)
                //{
                //    Filename = Regex.Replace(name, @"\s+", "") + cdatetime + ".pdf";
                //    owner = name;
                //}
                //else
                //{
                //    Filename = Regex.Replace(ReferanceId, @"\s+", "") + cdatetime + ".pdf";
                //    owner = "_ _ _ _ _ _ _ _ _ _ _ _";
                //}

                var guid = Guid.NewGuid().ToString().Split('-');
                string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";
                var url = string.Format("http://api.qrserver.com/v1/create-qr-code/?data=" + ReferanceId);
                WebResponse response = default(WebResponse);
                Stream remoteStream = default(Stream);
                StreamReader readStream = default(StreamReader);
                WebRequest request = WebRequest.Create(url);
                response = request.GetResponse();
                remoteStream = response.GetResponseStream();
                readStream = new StreamReader(remoteStream);


                //Creating Path to save image in folder
                System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
                string imgpath = Path.Combine(Server.MapPath(AppDetails.basePath + AppDetails.CTPTQRCode), image_Guid);
                var exists = System.IO.Directory.Exists(Server.MapPath(AppDetails.basePath + AppDetails.CTPTQRCode));
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + AppDetails.CTPTQRCode));
                }
                img.Save(imgpath);
                response.Close();
                remoteStream.Close();
                readStream.Close();


                string GridHtml = string.Empty;
                // string GridHtml = "<style>table{border-collapse: collapse;}table, td, th { padding:8px;border: 1px solid black;}<style><div><center><p style='margin-top:0px'><img style='width:200px;height200px' src='http://sbaappynitty.co.in:4022/Content/images/img/app_icon_cms.png'/></p><p style='font-size:25px;font-weight:bold;margin:0px'>Ghanta Gadi</p><p style='font-size:25px;font-weight:bold;margin:0px'>" + AppDetails.AppName + "</p><br/><hr/><br/></center></div><div><center><p style='font-size:32px;margin-bottom:15px'>< b id = 'hide_Name' > " + owner + "</b ></p ></center><table align='center' class='table'> <tr> <td style='font-weight: bold;'>House Number  </td> <td style=''>" + number + "</td> </tr> <tr> <td style='font-weight: bold;'>Ward Number  </td> <td style=''>" + ward.WardNo+ "</td> </tr> <tr> <td style='font-weight: bold;'>Area  </td> <td style=''> " + area.Name + "</td> </tr> <tr> <td style='font-weight: bold;'> House Id  </td> <td style=''>" + ReferanceId + "</td> </tr> </table><center><br/><p style='font-weight: bold;'> Scan Scaniffy Code <br /><br/>< img class='img-responsive' id='imggg' alt='hoto Not Found'  src='" +string.Format("https://api.qrserver.com/v1/create-qr-code/?data=" + ReferanceId ) + "'/></p><br /></center><div class='modal-footer'></div></div>";
                if (SessionHandler.Current.AppId == 3068) // For Nagpur ULB
                {
                    string src = AppDetails.baseImageUrlCMS + "/Content/images/icons/Nagpur_logo.png";
                    //For Satana Only
                    GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #fff;border : 2px solid black;'><div style='text-align:center;padding-top: 5px;background: #abd037;'> <img style='width:250px;height:86px;' src='" + src + "'/> </div> <div style='font-size: 14px;background: #abd037;'><b> CTPT Id: " + ReferanceId + "</b> </div> <div style='height:10px;background: #fff;'></div><div style='background: #fff;'> <img style='width:245px;height:245px;' src='" + imgpath + "'/></div></div>";
                }
                else
                {
                    //string slogan = AppDetails.baseImageUrlCMS + "/Content/images/icons/slogan.png";
                    //string topimg = AppDetails.baseImageUrlCMS + "/content/images/icons/top_outdoor.png";
                    //string leftbottomimg = AppDetails.baseImageUrlCMS + "/Content/images/icons/left_outdoor.png";
                    //string dry_wet_new = AppDetails.baseImageUrlCMS + "/Content/images/icons/Khapa/dry&wet.png";

                    string top_img_new = AppDetails.baseImageUrlCMS + AppDetails.basePath + "Content/icons/Top_image.png";
                    string slogan_new = AppDetails.baseImageUrlCMS + AppDetails.basePath + "Content/icons/slogan.png";
                    string round = AppDetails.baseImageUrlCMS + AppDetails.basePath + "Content/icons/round.png";

                    //string top_img_new = "http://localhost:34557/" + AppDetails.basePath + "Content/icons/Top_image.png";
                    //string slogan_new = "http://localhost:34557/" + AppDetails.basePath + "Content/icons/slogan.png";
                    //string round = "http://localhost:34557/" + AppDetails.basePath + "Content/icons/round.png";

                    GridHtml = "<div style='width:100%;height: 100%;background:#ffffff;border : 2px solid #4fa30a;'><div style='float:left;width:7%;padding-top:110px;padding-left:8px;'><img src='" + round + "' style = 'width:20px;height:20px;margin-left:5px;'/></div><div style='float:left;width:58%;padding-left:16px;padding-top:7px;'><img src='" + imgpath + "' style = 'width:20px;height:20px;'/></div><div style='float:left;width:83%;padding-left:5px;padding-top:10px;padding-bottom:6px;'><div style='padding-left:5px;'><img style='width:150px;height:95px;' src='" + top_img_new + "'/></div><div style='text-align: center;font-weight: 900;padding-bottom:3px;'>&nbsp;&nbsp;&nbsp;<span style='color:#000000;text-align: center;font-size: 16px'>CTPT Id</span><br/><span style='color:#000000;text-align: center;font-size: 15px'>" + ReferanceId + "</span></div><div style='padding-left:5px;'><img src='" + slogan_new + "' style='width: 150px; height:49px;'/><br/></div></div><div style='float:left;width:3%;padding-top:110px;padding-left:22px;text-align:center;'><img src='" + round + "' style = 'width:20px;height:20px;'/></div></div>";
                }

                //string GridHtml = "<div style='width:100%;height: 100%;text-align: center;background: #fff;border : 2px solid black;'><div style='text-align:center;margin-top: 8px;font-size:22px;background: #abd037;'> O </div> <div style='background: #abd037;;font-weight: bold;font-size: 18px;'> " + AppDetails.AppName + "</div><div style='font-size: 15px;background: #abd037;'> House Id: " + ReferanceId + " </div><div style='height:10px;background: #abd037;'></div> <div style='height:10px;background: #fff;'></div><div style='background: #fff;'> <img style='width:250px;height:250px;' src='" + string.Format("https://api.qrserver.com/v1/create-qr-code/?data=" + ReferanceId) + "'/>  </div></div>";
                using (MemoryStream stream = new System.IO.MemoryStream())
                {
                    StringReader sr = new StringReader(GridHtml);
                    if (SessionHandler.Current.AppId == 3068)
                    {
                        var pgSize = new iTextSharp.text.Rectangle(216, 288);
                        Document pdfDoc = new Document(pgSize, 1f, 1f, 1f, 1f);
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();
                        var content = writer.DirectContent;
                        var pageBorderRect = new Rectangle(pdfDoc.PageSize);

                        pageBorderRect.Left += pdfDoc.LeftMargin;
                        pageBorderRect.Right -= pdfDoc.RightMargin;
                        pageBorderRect.Top -= pdfDoc.TopMargin;
                        pageBorderRect.Bottom += pdfDoc.BottomMargin;

                        content.SetColorStroke(BaseColor.BLACK);
                        content.SetLineWidth(5);
                        content.Rectangle(pageBorderRect.Left, pageBorderRect.Bottom, pageBorderRect.Width, pageBorderRect.Height);
                        content.Stroke();
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                        pdfDoc.Close();
                    }
                    else
                    {
                        var pgSize = new iTextSharp.text.Rectangle(324, 180);
                        Document pdfDoc = new Document(pgSize, 1f, 1f, 1f, 1f);
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();
                        var content = writer.DirectContent;
                        var pageBorderRect = new Rectangle(pdfDoc.PageSize);

                        pageBorderRect.Left += pdfDoc.LeftMargin;
                        pageBorderRect.Right -= pdfDoc.RightMargin;
                        pageBorderRect.Top -= pdfDoc.TopMargin;
                        pageBorderRect.Bottom += pdfDoc.BottomMargin;

                        content.SetColorStroke(BaseColor.BLACK);
                        content.SetLineWidth(5);
                        content.Rectangle(pageBorderRect.Left, pageBorderRect.Bottom, pageBorderRect.Width, pageBorderRect.Height);
                        content.Stroke();
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                        pdfDoc.Close();
                    }
                    return File(stream.ToArray(), "application/pdf", Filename);
                }

            }
            else
                return Redirect("/Account/Login");
        }

    }
}