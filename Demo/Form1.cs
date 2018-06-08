using Demo.Controls;
using Demo.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using WebWeixin;
using WebWeixin.Models;
using WebWeixin.Utils;

namespace Demo
{
    public partial class Form1 : Form
    {
        WeiXinSdk wxsdk;
        List<Contact> contacts;
        ContactItem CurrentContact { get; set; }
        User CurrentUser { get; set; }
        public Form1()
        {
            InitializeComponent();
            wxsdk = new WeiXinSdk();
            wxsdk.ImageChanged += Wxsdk_ImageChanged;
            wxsdk.AjaxCompleted += Wxsdk_AjaxCompleted;
            contacts = new List<Contact>();
        }


        private void Wxsdk_AjaxCompleted(AjaxResult result)
        {
            switch (result.AjaxType)
            {
                case AjaxTypes.WxInit:
                    WxInit(result.Data as WebwxInitInfo);
                    break;
                case AjaxTypes.WxContact:
                    Task.Run(() => {
                        WxContact(result.Data as WebwxContactInfo);
                    });
                    break;
                case AjaxTypes.WxSync:
                    WxSync(result.Data as WebwxSyncInfo);
                    break;
                case AjaxTypes.WxBatchGetContact:
                    WxBatchContacts(result.Data as WebwxContactInfo, result.DataKey);
                    break;
            }
        }
        private void WxBatchContacts(WebwxContactInfo cinfo,string uname)
        {
            if (!string.IsNullOrWhiteSpace(uname))
            {
                foreach (var c in panUserList.Controls)
                {
                    ContactItem citem = c as ContactItem;
                    if (citem.Name == uname)
                    {
                        citem.ContactList = cinfo.MemberList;
                        break;
                    }
                }
            }
            else
            {
                foreach(var m in cinfo.MemberList)
                {
                    foreach (var c in panUserList.Controls)
                    {
                        ContactItem citem = c as ContactItem;
                        if (citem.Name == m.UserName)
                        {
                            citem.ContactInfo = m;
                            break;
                        }
                    }
                }
            }
        }
        //消息同步
        private void WxSync(WebwxSyncInfo info)
        {
            if (info.AddMsgCount > 0)
            {

                foreach (var c in panUserList.Controls)
                {
                    ContactItem citem = c as ContactItem;
                    var msgs = info.AddMsgList.Where(x => x.FromUserName == citem.Name || x.ToUserName == citem.Name).ToList();
                    foreach (var msg in msgs)
                    {
                        MsgShowInfo msginfo = new MsgShowInfo
                        {
                            Content = msg.Content,
                            FromUser = msg.FromUserName,
                            ToUser = msg.ToUserName,
                            isMe = msg.FromUserName == CurrentUser.UserName,
                            MsgType = msg.MsgType,
                        };
                        if (msginfo.isMe)
                        {
                            msginfo.HeadImg = pictureBox1.Image;
                        }
                        else if (msg.FromUserName == citem.Name)
                        {
                            msginfo.HeadImg = citem.Avatar;
                        }
                        citem.msgList.Add(msginfo);
                        if(CurrentContact?.Name== citem.Name)
                        {
                            msgListShow1.Add(msginfo);
                            if (autoReply.Checked)
                            {
                                SendMsg(citem.Name, "这是自动回复的消息:"+DateTime.Now.ToString());
                            }
                        }
                        break;
                    }
                }
            }
        }
        //获取联系人
        private void WxContact(WebwxContactInfo info)
        {

            foreach (Contact c in info.MemberList)
            {
                if (contacts.Find(x => x.UserName == c.UserName) != null)
                {
                    continue;
                }
                Application.DoEvents();
                Thread.Sleep(100);
                ContactAppend(c,true);
            }
        }
        //初始化
        private void WxInit(WebwxInitInfo initInfo)
        {
            MainShow();
            
            panUserList.Controls.Clear();
            contacts.Clear();
            CurrentUser = initInfo.User;
            foreach (Contact c in initInfo.ContactList)
            {
                ContactAppend(c,true);
            }
        }
        //追加联系人
        private void ContactAppend(Contact c,bool headimg=false)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(delegate
                {
                    ContactAppend(c, headimg);
                }));
                return;
            }
            if (contacts.Find(x => x.UserName == c.UserName) != null)
            {
                return;
            }
            contacts.Add(c);
            ContactItem item = new ContactItem(c);
            item.Name = c.UserName;
            if (headimg)
            {
                wxsdk.DownFile(c.HeadImgUrl, (byte[] img) =>
                {
                    item.Avatar = BytesToImage(img);
                    item.HeadImgUpdateFlag = true;
                });
            }
            item.Dock = DockStyle.Top;
            item.Click += Item_Click;
            panUserList.Controls.Add(item);
            item.BringToFront();

        }
        //点击左侧联系人
        private void Item_Click(object sender, EventArgs e)
        {
            ContactItem item = sender as ContactItem;
            var info = item.ContactInfo;
            if (!item.HeadImgUpdateFlag)
            {
                wxsdk.DownFile(info.HeadImgUrl, (byte[] img) =>
                {
                    item.Avatar = BytesToImage(img);
                    item.HeadImgUpdateFlag = true;
                });
            }
            labTitle.Text = item.NickName;
            if (info.MemberCount > 0)
            {
                labTitle.Text = item.NickName + "(" + info.MemberCount.ToString() + ") v ";
                if (item.ContactList == null)
                {
                    List<Contact> clist = new List<Contact>();
                    foreach (var member in info.MemberList)
                    {
                        clist.Add(new Contact
                        {
                            EncryChatRoomId = info.EncryChatRoomId,
                            UserName = member.UserName
                        });
                    }
                    wxsdk.BatchGetContact(clist, info.UserName);
                }
            }
            CurrentContact = item;
            wxsdk.SendStatusNotify(1, CurrentUser.UserName, CurrentContact.ContactInfo.UserName);
            msgListShow1.FillList(item.msgList);

        }
        //字节数组转图片
        private Image BytesToImage(byte[] bs)
        {
            try
            {
                if (bs == null || bs.Length == 0)
                {
                    return null;
                }
                MemoryStream ms = new MemoryStream(bs);
                Image image = Image.FromStream(ms);
                return image;
            }
            catch
            {
                return null;
            }

        }
        //登录扫码图片时间
        private void Wxsdk_ImageChanged(Image img)
        {
            pictureBox1.Image = img;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoginShow();
            //MainShow();
            
        }
        //显示登录
        private  void LoginShow()
        {
            Text = "扫码登录";
            panMain.Visible = false;
            Width = 300;
            Height = 350;
            panLogin.Visible = true;
            panLogin.Dock = DockStyle.Fill;
            OffsetCenter();
        }
        //显示主窗口
        private void MainShow()
        {
            Text = wxsdk.UserInfo?.NickName;
            panLogin.Visible = false;

            panMain.Visible = true;
            Width = 800;
            Height = 600;
            panMain.Dock = DockStyle.Fill;
            OffsetCenter();
        }
        //居中
        private void OffsetCenter()
        {
            int sw = Screen.PrimaryScreen.Bounds.Width;
            int sh = Screen.PrimaryScreen.Bounds.Height;

            Left = (sw - Width) / 2;
            Top = (sh - Height) / 2;
        }
        //发消息
        private void btn_send_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMsg.Text)||string.IsNullOrWhiteSpace(CurrentContact?.ContactInfo?.UserName))
            {
                return;
            }

            SendMsg(CurrentContact?.ContactInfo?.UserName, txtMsg.Text);
            txtMsg.Text = "";
        }
        private void SendMsg(string touser,string msg)
        {
            var minfo = new MsgShowInfo
            {
                Content = msg,
                FromUser = CurrentUser.UserName,
                ToUser = touser,
                HeadImg = pictureBox1.Image,
                MsgType = 1,
                NickName = CurrentUser.NickName,
                isMe = true,
            };
            var result = wxsdk.SendMsg(minfo.ToUser, msg);
            if (result.BaseResponse.Ret == 0)
            {
                var ctrls = panUserList.Controls.Find(touser, false);
                if (ctrls.Length > 0)
                {
                    var cntItem = ctrls[0] as ContactItem;
                    cntItem?.msgList.Add(minfo);
                    if (touser == CurrentContact?.Name)
                    {
                        msgListShow1.Add(minfo);
                    }
                }

            }
        }
        //测试
        private void test()
        {
            CurrentUser = new User();
            CurrentUser.UserName = "isMe";
            List<MsgShowInfo> list = new List<MsgShowInfo>();
            list.Add(new MsgShowInfo
            {
                Content = "msg 1",
                FromUser = "isMe",
                NickName = "uuuu",
                ToUser = "you",
                MsgType = 0,
                HeadImg = Image.FromFile(@"E:\source\repos\h5xiquan\img\active.png"),
                isMe = true,
            });
            list.Add(new MsgShowInfo
            {
                Content = "msg 2",
                FromUser = "you",
                NickName = "uuuu",
                ToUser = "isMe",
                MsgType = 0,
                HeadImg = Image.FromFile(@"E:\source\repos\h5xiquan\img\bird.png"),
                isMe = false,
            });

            list.Add(new MsgShowInfo
            {
                Content = "2种结果都一样，这是我们不希望得到的，我们希望从左边开始，第一个出现</h3>，就开始匹配。以上这种模式，是贪婪模式，也是正则表达式默认以这个方法匹配。表示重复字符，操作符，默认都是贪婪模式，如：.*,.+,.{1,},.{0,} 都会匹配最大长度字符。正则表达式元字符，量词默认首先最大匹配字符串，这些量词有：+,*,?,{m,n} 。一开始匹配，就直接匹配到最长字符串。",
                FromUser = "isMe",
                NickName = "uuuu",
                ToUser = "uuuu",
                MsgType = 0,
                HeadImg = Image.FromFile(@"E:\source\repos\h5xiquan\img\active.png"),
                isMe = true,
            });
            list.Add(new MsgShowInfo
            {
                Content = "msg 4",
                FromUser = "you",
                NickName = "uuuu",
                ToUser = "uuuu",
                MsgType = 0,
                HeadImg = Image.FromFile(@"E:\source\repos\h5xiquan\img\bird.png"),
                isMe = false,
            });
            list.Add(new MsgShowInfo
            {
                Content = "既然上面几种，表示字符重复个数，元字符默认都是贪婪模式。如果，我们需要最小长度匹配，也就是懒惰模式，怎么样写正则表达式呢？其实，正则表达式里面通用方法是，在表示重复字符元字符，后面加多一个”?”字符即可。上面正则表达式可以写成",
                FromUser = "you",
                NickName = "uuuu",
                ToUser = "isMe",
                MsgType = 0,
                HeadImg = Image.FromFile(@"E:\source\repos\h5xiquan\img\bird.png"),
                isMe = false,
            });
            list.Add(new MsgShowInfo
            {
                Content = "2种结果都一样，这是我们不希望得到的，我们希望从左边开始，第一个出现</h3>，就开始匹配。以上这种模式，是贪婪模式，也是正则表达式默认以这个方法匹配。表示重复字符，操作符，默认都是贪婪模式，如：.*,.+,.{1,},.{0,} 都会匹配最大长度字符。正则表达式元字符，量词默认首先最大匹配字符串，这些量词有：+,*,?,{m,n} 。一开始匹配，就直接匹配到最长字符串。",
                FromUser = "isMe",
                NickName = "uuuu",
                ToUser = "uuuu",
                MsgType = 0,
                HeadImg = Image.FromFile(@"E:\source\repos\h5xiquan\img\active.png"),
                isMe = true,
            });
            list.Add(new MsgShowInfo
            {
                Content = "msg 4",
                FromUser = "you",
                NickName = "uuuu",
                ToUser = "uuuu",
                MsgType = 0,
                HeadImg = Image.FromFile(@"E:\source\repos\h5xiquan\img\bird.png"),
                isMe = false,
            });
            list.Add(new MsgShowInfo
            {
                Content = "既然上面几种，表示字符重复个数，元字符默认都是贪婪模式。如果，我们需要最小长度匹配，也就是懒惰模式，怎么样写正则表达式呢？其实，正则表达式里面通用方法是，在表示重复字符元字符，后面加多一个”?”字符即可。上面正则表达式可以写成",
                FromUser = "you",
                NickName = "uuuu",
                ToUser = "isMe",
                MsgType = 0,
                HeadImg = Image.FromFile(@"E:\source\repos\h5xiquan\img\bird.png"),
                isMe = false,
            });
            msgListShow1.FillList(list);
        }
        //开始登录
        private void btn_login_Click(object sender, EventArgs e)
        {
            wxsdk.StartLogin();
            
        }
    }
}
