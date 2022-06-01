

<html>

    <head><link rel="alternate" type="text/xml" href="/WebServices/WeatherWebService.asmx?disco" />

    <style type="text/css">
    
		BODY { color: #000000; background-color: white; font-family: Verdana; margin-left: 0px; margin-top: 0px; }
		#content { margin-left: 30px; font-size: .70em; padding-bottom: 2em; }
		A:link { color: #336699; font-weight: bold; text-decoration: underline; }
		A:visited { color: #6699cc; font-weight: bold; text-decoration: underline; }
		A:active { color: #336699; font-weight: bold; text-decoration: underline; }
		A:hover { color: cc3300; font-weight: bold; text-decoration: underline; }
		P { color: #000000; margin-top: 0px; margin-bottom: 12px; font-family: Verdana; }
		pre { background-color: #e5e5cc; padding: 5px; font-family: Courier New; font-size: x-small; margin-top: -5px; border: 1px #f0f0e0 solid; }
		td { color: #000000; font-family: Verdana; font-size: .7em; }
		h2 { font-size: 1.5em; font-weight: bold; margin-top: 25px; margin-bottom: 10px; border-top: 1px solid #003366; margin-left: -15px; color: #003366; }
		h3 { font-size: 1.1em; color: #000000; margin-left: -15px; margin-top: 10px; margin-bottom: 10px; }
		ul { margin-top: 10px; margin-left: 20px; }
		ol { margin-top: 10px; margin-left: 20px; }
		li { margin-top: 10px; color: #000000; }
		font.value { color: darkblue; font: bold; }
		font.key { color: darkgreen; font: bold; }
		font.error { color: darkred; font: bold; }
		.heading1 { color: #ffffff; font-family: Tahoma; font-size: 26px; font-weight: normal; background-color: #003366; margin-top: 0px; margin-bottom: 0px; margin-left: -30px; padding-top: 10px; padding-bottom: 3px; padding-left: 15px; width: 105%; }
		.button { background-color: #dcdcdc; font-family: Verdana; font-size: 1em; border-top: #cccccc 1px solid; border-bottom: #666666 1px solid; border-left: #cccccc 1px solid; border-right: #666666 1px solid; }
		.frmheader { color: #000000; background: #dcdcdc; font-family: Verdana; font-size: .7em; font-weight: normal; border-bottom: 1px solid #dcdcdc; padding-top: 2px; padding-bottom: 2px; }
		.frmtext { font-family: Verdana; font-size: .7em; margin-top: 8px; margin-bottom: 0px; margin-left: 32px; }
		.frmInput { font-family: Verdana; font-size: 1em; }
		.intro { margin-left: -15px; }
           
    </style>

    <title>
	WeatherWebService Web 服务
</title></head>

  <body>

    <div id="content">

      <p class="heading1">WeatherWebService</p><br>

      <span>
          <p class="intro"><a href="http://www.webxml.com.cn/" target="_blank">WebXml.com.cn</a> <strong>天气预报 Web 服务，数据每2.5小时左右自动更新一次，准确可靠。包括 340 多个中国主要城市和 60 多个国外主要城市三日内的天气预报数据。</br>此天气预报Web Services请不要用于任何商业目的，若有需要请<a href="http://www.webxml.com.cn/zh_cn/contact_us.aspx" target="_blank">联系我们</a>，欢迎技术交流。 QQ：8409035<br />使用本站 WEB 服务请注明或链接本站：http://www.webxml.com.cn/ 感谢大家的支持</strong>！<br /><span style="color:#999999;">通知：天气预报 WEB 服务如原来使用地址 http://www.onhap.com/WebServices/WeatherWebService.asmx 的，请改成现在使用的服务地址 http://www.webxml.com.cn/WebServices/WeatherWebService.asmx ，重新引用即可。</span><br /><br />&nbsp;</p>
      </span>

      <span>

          <p class="intro">支持下列操作。有关正式定义，请查看<a href="WeatherWebService.asmx?WSDL">服务说明</a>。 </p>
          
          
              <ul>
            
              <li>
                <a href="WeatherWebService.asmx?op=getSupportCity">getSupportCity</a>
                
                <span>
                  <br><br /><h3>查询本天气预报Web Services支持的国内外城市或地区信息</h3><p>输入参数：byProvinceName = 指定的洲或国内的省份，若为ALL或空则表示返回全部城市；返回数据：一个一维字符串数组 String()，结构为：城市名称(城市代码)。</p><br />
                </span>
              </li>
              <p>
            
              <li>
                <a href="WeatherWebService.asmx?op=getSupportDataSet">getSupportDataSet</a>
                
                <span>
                  <br><br><h3>获得本天气预报Web Services支持的洲、国内外省份和城市信息</h3><p>输入参数：无；返回：DataSet 。DataSet.Tables(0) 为支持的洲和国内省份数据，DataSet.Tables(1) 为支持的国内外城市或地区数据。DataSet.Tables(0).Rows(i).Item("ID") 主键对应 DataSet.Tables(1).Rows(i).Item("ZoneID") 外键。<br />Tables(0)：ID = ID主键，Zone = 支持的洲、省份；Tables(1)：ID 主键，ZoneID = 对应Tables(0)ID的外键，Area = 城市或地区，AreaCode = 城市或地区代码。</p><br />
                </span>
              </li>
              <p>
            
              <li>
                <a href="WeatherWebService.asmx?op=getSupportProvince">getSupportProvince</a>
                
                <span>
                  <br><br /><h3>获得本天气预报Web Services支持的洲、国内外省份和城市信息</h3><p>输入参数：无； 返回数据：一个一维字符串数组 String()，内容为洲或国内省份的名称。</p><br />
                </span>
              </li>
              <p>
            
              <li>
                <a href="WeatherWebService.asmx?op=getWeatherbyCityName">getWeatherbyCityName</a>
                
                <span>
                  <br><br><h3>根据城市或地区名称查询获得未来三天内天气情况、现在的天气实况、天气和生活指数</h3><p>调用方法如下：输入参数：theCityName = 城市中文名称(国外城市可用英文)或城市代码(不输入默认为上海市)，如：上海 或 58367，如有城市名称重复请使用城市代码查询(可通过 getSupportCity 或 getSupportDataSet 获得)；返回数据： 一个一维数组 String(22)，共有23个元素。<br />String(0) 到 String(4)：省份，城市，城市代码，城市图片名称，最后更新时间。String(5) 到 String(11)：当天的 气温，概况，风向和风力，天气趋势开始图片名称(以下称：图标一)，天气趋势结束图片名称(以下称：图标二)，现在的天气实况，天气和生活指数。String(12) 到 String(16)：第二天的 气温，概况，风向和风力，图标一，图标二。String(17) 到 String(21)：第三天的 气温，概况，风向和风力，图标一，图标二。String(22) 被查询的城市或地区的介绍 <br /><a href="http://www.webxml.com.cn/images/weather.zip">下载天气图标<img src="http://www.webxml.com.cn/images/download_w.gif" border="0" align="absbottom" /></a>(包含大、中、小尺寸) <a href="http://www.webxml.com.cn/zh_cn/weather_icon.aspx" target="_blank">天气图例说明</a> <a href="http://www.webxml.com.cn/files/weather_eg.zip">调用此天气预报Web Services实例下载</a> (VB ASP.net 2.0)</p><br />
                </span>
              </li>
              <p>
            
              <li>
                <a href="WeatherWebService.asmx?op=getWeatherbyCityNamePro">getWeatherbyCityNamePro</a>
                
                <span>
                  <br><br><h3>根据城市或地区名称查询获得未来三天内天气情况、现在的天气实况、天气和生活指数（For商业用户）</h3><p>调用方法同 getWeatherbyCityName，输入参数：theUserID = 商业用户ID</p><br />
                </span>
              </li>
              <p>
            
              </ul>
            
      </span>

      
      

    <span>
        
    </span>
    
      

      

    
  </body>
</html>
