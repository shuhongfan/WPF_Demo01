// js实现修改div里img标签的src属性
//数据来源：http://wthrcdn.etouch.cn/weather_mini?city=孝感
/*
{"data":{"yesterday":{"date":"15日星期六","high":"高温 26℃","fx":"西风","low":"低温 18℃","fl":"","type":"中雨"},"city":"武汉","forecast":[{"date":"16日星期天","high":"高温 20℃","fengli":"","low":"低温 14℃","fengxiang":"北风","type":"小雨"},{"date":"17日星期一","high":"高温 23℃","fengli":"","low":"低温 14℃","fengxiang":"北风","type":"多云"},{"date":"18日星期二","high":"高温 23℃","fengli":"","low":"低温 15℃","fengxiang":"南风","type":"阴"},{"date":"19日星期三","high":"高温 22℃","fengli":"","low":"低温 17℃","fengxiang":"东南风","type":"中雨"},{"date":"20日星期四","high":"高温 28℃","fengli":"","low":"低温 18℃","fengxiang":"西南风","type":"多云"}],"ganmao":"感冒低发期，天气舒适，请注意多吃蔬菜水果，多喝水哦。","wendu":"15"},"status":1000,"desc":"OK"}
*/

//页面第一次显示的时候显示当地的天气信息
window.onload = function () {
    var native = new BMap.LocalCity();
    // alert(native.name);
    var local;
    native.get(function(r){
        local=r.name;
        getWeatherInfo(local);
    });

}

function getWeatherInfo(city) {
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
    xmlhttp.open("GET", "http://wthrcdn.etouch.cn/weather_mini?city=" + city, true);
    //发送请求
    xmlhttp.send();
    //响应
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {

            //alert(xmlhttp.responseText);
            var data1 = xmlhttp.responseText;
            var json = JSON.parse(data1);


            var oSpan = document.getElementsByClassName('info');
            oSpan[0].innerHTML = json.data.city; //城市
            oSpan[1].innerHTML = getNowFormatDate();//日期
            oSpan[2].innerHTML = json.data.forecast[0].date.substr(length - 3);	//星期
            //alert(json.data.forecast[0].type);
            oSpan[3].innerHTML = json.data.forecast[0].type; //天气
            if (json.data.forecast[0].type == '小雨') {
                document.body.style.backgroundImage = "url(image/n03.png)";
                document.getElementById('myimg').src = "image/a_7.gif";
            } else if (json.data.forecast[0].type == '阴') {
                document.body.style.backgroundImage = "url(image/n02.png)";
                document.getElementById('myimg').src = "image/1.gif";
            } else if (json.data.forecast[0].type == '晴') {
                document.body.style.backgroundImage = "url(image/n00.png)";
                document.getElementById('myimg').src = "image/0.gif";
            }
            //有待补充其他天气及其图片

            oSpan[4].innerHTML = json.data.forecast[0].high + "/" + json.data.forecast[0].low;
            oSpan[5].innerHTML = json.data.forecast[0].fengli.substr(length - 5, 2);
            oSpan[6].innerHTML = json.data.forecast[0].fengxiang;
            //下面修改5天内的天气
            var oSpan1 = document.getElementsByClassName('future_info1');
            oSpan1[0].innerHTML = getNowFormatDate();//日期
            oSpan1[1].innerHTML = json.data.forecast[0].date.substr(length - 3);	//星期
            oSpan1[2].innerHTML = json.data.forecast[0].type;
            if (json.data.forecast[0].type == '小雨') {
                document.getElementById('myimg1').src = "image/a_7.gif";
            }
            oSpan1[3].innerHTML = json.data.forecast[0].high + "-" + json.data.forecast[0].low;


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
				if (type=='小雨') boxs[i].childNodes[5].src="image/a_7.gif";
				else if (type=='多云') boxs[i].childNodes[5].src="image/2.gif";
				else if (type=='小雨') boxs[i].childNodes[5].src="image/a_7.gif";
				else if (type=='晴') boxs[i].childNodes[5].src="image/0.gif";
				else if (type=='大雨') boxs[i].childNodes[5].src="image/a_12.gif";
				boxs[i].childNodes[7].innerText=type
				boxs[i].childNodes[8].innerText=low.split(" ")[1]+"-"+high.split(" ")[1]
			}
        }
    }
    //请求天气车数据
    btn.addEventListener('click', function () {
        var city = document.getElementById('text').value;
        getWeatherInfo(city);


    });
    text.addEventListener('keydown', function (e) {
        if (e.keyCode == 13) {
            var city = document.getElementById('text').value;
            getWeatherInfo(city);
        }
    });
}

//获取日期的函数
function getNowFormatDate() {
    var day = new Date();
    var Year = 0;
    var Month = 0;
    var Day = 0;
    var CurrentDate = "";
//初始化时间
//Year= day.getYear();//有火狐下2008年显示108的bug
    Year = day.getFullYear();//ie火狐下都可以
    Month = day.getMonth() + 1;
    Day = day.getDate();
//Hour = day.getHours();
// Minute = day.getMinutes();
// Second = day.getSeconds();
    CurrentDate += Year + "-";
    if (Month >= 10) {
        CurrentDate += Month + "-";
    } else {
        CurrentDate += "0" + Month + "-";
    }
    if (Day >= 10) {
        CurrentDate += Day;
    } else {
        CurrentDate += "0" + Day;
    }
    return CurrentDate;
}
