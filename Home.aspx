<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $("#Menu_MainOptions li").on("click", function () {
            $("#Menu_MainOptions li").removeClass("staticMenuStyle");
            $(this).addClass("staticMenuStyle");
        });
</script>

            <style type="text/css">
         html { min-height: 100%;min-width: 100% ;padding:0px; margin:0px}
body {min-height: 100%; min-width: 100%; padding:0px; margin:0px  }

  .style1
        {
            width: 100%;
                
                margin-bottom: 0px;
            }
.style3
        {
            width:70%;
            padding: 0px;
             margin: 0px;
                
            }
            .style4
        {
            width:30%;
              padding: 0px;
             margin: 0px;
               
            }
            .staticMenuStyle
          {background-color: #fff; margin: 5px 0 10px 0; 
            }
            
            
            #content {
    position: fixed;
    top: 0;
    left: 0;
    height: 100%;
            width: 100%;
        }
html { min-height: 100%; }
body {min-height: 100%; }
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="content">    
   <iframe id="Iframe1" name="Content"  src="ContentPlacer.aspx" height="100%" width="100%"></iframe>
</div>
    </form>
</body>
</html>
