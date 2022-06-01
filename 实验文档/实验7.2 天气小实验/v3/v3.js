//数据来源：http://wthrcdn.etouch.cn/weather_mini?city=孝感
/*
{"data":{"yesterday":{"date":"15日星期六","high":"高温 26℃","fx":"西风","low":"低温 18℃","fl":"","type":"中雨"},"city":"武汉","forecast":[{"date":"16日星期天","high":"高温 20℃","fengli":"","low":"低温 14℃","fengxiang":"北风","type":"小雨"},{"date":"17日星期一","high":"高温 23℃","fengli":"","low":"低温 14℃","fengxiang":"北风","type":"多云"},{"date":"18日星期二","high":"高温 23℃","fengli":"","low":"低温 15℃","fengxiang":"南风","type":"阴"},{"date":"19日星期三","high":"高温 22℃","fengli":"","low":"低温 17℃","fengxiang":"东南风","type":"中雨"},{"date":"20日星期四","high":"高温 28℃","fengli":"","low":"低温 18℃","fengxiang":"西南风","type":"多云"}],"ganmao":"感冒低发期，天气舒适，请注意多吃蔬菜水果，多喝水哦。","wendu":"15"},"status":1000,"desc":"OK"}
*/

    window.onload=search()
	//获取当前日期
	function getymd()
	{
    var date = new Date();
    var mm = date.getMonth() + 1; //月
    var dd = date.getDate(); //日
    var yy = date.getFullYear(); //年
    return yy + "." + mm + "." + dd;
   }

   function search() {
       //创建对象
       var xmlhttp;
       if (window.XMLHttpRequest) {
           //  IE7+, Firefox, Chrome, Opera, Safari 浏览器执行代码
           xmlhttp = new XMLHttpRequest();
       } else {
           // IE6, IE5 浏览器执行代码
           xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
       }
       //建立连接
       let text = document.getElementById("text").value;
       if (text==""||text==null) text='武汉'
       xmlhttp.open("GET", "http://wthrcdn.etouch.cn/weather_mini?city="+text, true);
       //发送请求
       xmlhttp.send();
       //响应
       xmlhttp.onreadystatechange = function () {
           if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
               //alert(xmlhttp.responseText);
               var data1 =xmlhttp.responseText;
               var json = JSON.parse(data1);
               console.log(json);

               //alert(json.data.yesterday.date); //日期+星期
               //alert(json.data.yesterday.high); //高温
               //alert(json.data.yesterday.fx); //风向
               //alert(json.data.yesterday.fl); //风力
               // alert(json.data.forecast[0].date);当天
               var oSpan = document.getElementsByClassName('info');
               oSpan[0].innerHTML = json.data.city;
               oSpan[1].innerHTML = getymd(); //显示当前日期
               oSpan[2].innerHTML = json.data.forecast[0].date.substr(length -3);
               //alert(json.data.forecast[0].type);
               oSpan[3].innerHTML = json.data.forecast[0].type;
               oSpan[4].innerHTML = json.data.forecast[0].high+"/"+ json.data.forecast[0].low;
               oSpan[5].innerHTML = json.data.forecast[0].fengli;
               oSpan[6].innerHTML = json.data.forecast[0].fengxiang;

               let boxs= document.getElementsByClassName("future_box")
               console.log(boxs)

               for (let i = 0; i < boxs.length; i++) {
                   let childNodes = boxs[i].childNodes;
                   let date = json.data.forecast[i].date;
                   let high = json.data.forecast[i].high;
                   let low = json.data.forecast[i].low;
                   let fengxiang = json.data.forecast[i].fengxiang;
                   let fengli = json.data.forecast[i].fengli;
                   let type = json.data.forecast[i].type;
                   console.log(boxs[i].childNodes)

                   let splice = date.split("日");
                   let d = new Date();
                   boxs[i].childNodes[1].innerText=d.getFullYear()+"-"+(d.getMonth()+1)+"-"+splice[0]
                   boxs[i].childNodes[3].innerText=splice[1]
                   boxs[i].childNodes[5].innerText=type
                   boxs[i].childNodes[7].innerText=type
                   boxs[i].childNodes[8].innerText=low.split(" ")[1]+"-"+high.split(" ")[1]
               }
           }
       }
   }

