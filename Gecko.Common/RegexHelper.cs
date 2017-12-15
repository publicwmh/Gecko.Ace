﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Gecko.Common
{
    public class RegexHelper
    {
        #region Replace

        public static MatchCollection Matches(string input, string pattern)
        {
            Regex rex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.ExplicitCapture);

            MatchCollection mcs = rex.Matches(input);

            return mcs;
        }


        public static string GetMatches(string input, string pattern)
        {
            Regex rex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.ExplicitCapture);

            MatchCollection mcs = rex.Matches(input);

            if (mcs.Count > 0)
                return mcs[0].Groups["contents"].Value;
            else
                return "";
        }


        /// <summary>
        /// 得到字符串
        /// </summary>
        /// <param name="input">源</param>
        /// <param name="regStart">前字符</param>
        /// <param name="regEnd">后字符</param>
        /// <returns></returns>
        public static string GetMatches(string input, string regStart, string regEnd)
        {
            string regString = string.Format("{0}(?<getcontent>[\\s|\\S]+?){1}", regStart, regEnd);
            MatchCollection matches = Matches(input, regString);
            string results = "";
            if (matches.Count > 0)
            {
                for (int i = 0; i < matches.Count; i++)
                {
                    results = results + "|" + matches[i].Groups["getcontent"].Value;
                }
                results = results.Substring(1);
            }
            else
            {
                results = "";
            }
            return results;
        }


        //是否匹配
        public static Boolean IsMatch(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }


        public static string Replace(string input, string pattern, string replacement)
        {
            return Regex.Replace(input, pattern, replacement, RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }

        //替换非法词为**
        public static string replaceFeiFaChi(string input)
        {
            return Replace(input, keyWords(), "**");
        }

        public static string keyWords()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"12次下跪|18岁以下勿入看|27军军长|AV电影|AV女|AV女优|A级|A片|dafa|falun|falundafa|go-vern-ment|Government|Party|PK黑社会|PX项目|px项目|SM虐待|todaynic|zhuanfalu|阿兵哥言语录|阿里布达年代记|爱神之传奇|安眠药|罢餐|罢吃|罢饭|罢食|白小姐|百家乐|办理文凭|办理证件|办证|保钓|北京奥运会|本拉登|冰毒|波波|波佳达|博白县|博彩|不良少女日记|部巡查|彩票机|沧澜曲|藏独|操你妈|车牌反光|陈海石|陈良宇罢官|成人|成人电影|成人卡通|成人漫画|成人片|成人图片|成人文学|成人小说|吃角子老虎|赤裸|臭作|出墙红杏|出售|出售假币|出售枪支|出售手枪|处女|创世之子猎艳之旅|春宵|春药|崔英杰|催情药|达赖|打KING|打倒|大法|大法弟子|大纪元|大紀元|大揭露|大陆当局|大陆当权者|大陆独裁者|大陆官员|大学骚乱|代开|代开发票|戴海静|单管猎枪|党代|盗电|盗窃|盗取|道县公安|邓小平|抵制家乐福|抵制日货|帝国之梦|第十六次代表|第十六屆|第十六届|第拾陆届|点对点裸聊|电车之狼|电狗|电子地磅解码器|调情|东北风情熟女之惑|东突|動乱|豆蔻|毒品|赌博|赌场|赌城|赌钱|赌球|赌骰子|短信猫|短信群发器|对日强硬|多党执政|恶搞晚会|二奶大奖赛|二奶大赛|发票|法lun功|法轮|法轮大法|法轮功|法輪功|反动|反动2|反革命|反共|反华|反右题材|反政府|反中游行|仿54|仿64|仿六四|仿五四|仿真假钞|仿真枪|肥东交警|分裂国家|风尘劫|风骚|风骚侍女|风月大陆|疯狂盗窃|夫妇乐园|扶不起的阿斗|富人与农民工|干柴烈火|干你娘|敢坐飞机吗|赣江学院|肛交|高潮|高干子弟|高干子女|高校暴乱|高校群体事件|高校骚乱|高莺莺|各种枪|公安|公安部巡查|公务员的工资|公务员调资|公务员工资|攻击党政|供应猎枪|共产党|共产主义|共铲党|古方迷香|股市民谣|官逼民反|官商勾结|广安第二人民医院|广东王|鬼村|国际足坛五大|国民党|嗨妹|嗨药|海盗的悠闲生活|海乐神|酣乐欣|韩国身份证|和奸成瘾|和弦|黑天使|黑星女侠|黑窑奴役|红wy|红海湾|红楼绮梦|胡紧掏|胡锦涛|胡錦濤|胡景涛|胡新宇|胡耀|胡主席|换妻|黄片|黄色|黄色电影|黄色免费电影|黄色小电影|挥发型迷药|昏药|混蛋神风流史|火药制作|鸡巴|鸡吧|激情|激情聊天|激情视频|激情图片|激情小电影|激情秀|激情淫|级片|极景|计生风暴|纪股票市场五卅惨案|妓女|济南建设路|佳静安定片|甲基苯丙胺|假币|假钞|奸淫|监听器|监听王|践踏中国女性|江湖淫娘|江山美人志|江氏|江西田园置业集团|江泽民|江主席|疆独|蒋彦永|狡猾的风水相师|搅珠|叫床|金伯帆|金伯帆崔英杰|金鳞岂是池中物|锦涛|禁断少女|禁片|禁区|禁书|精液|九评|菊花洞|巨波|巨额骗储|军长发威|军火价格|开彩|科技精英遇难|嗑药|口交|口头检查|狂风暴雨|拉登|拉登说中国|拉凳|拦截器|老虎|老虎机|累积大奖|黎阳平|黎阳评|礼品机|李宏志|李洪志|李沛瑶|李鹏|俪影蝎心|连线机|梁保华|两性狂情、|猎枪子弹|溜冰|刘全喜|流氓无产者|劉奇葆|六合|六合采|六合彩|六合彩1|六决不|六码|六四|六四母亲|六四事件|六四屠城|六四屠杀|六四学潮|六四血案|六四政变|六四之|卢跃刚|乱伦|伦理片|轮奸|轮盘机|裸聊合法|裸体|裸体写真|骆冰淫传|麻古|麻果|麻醉钢枪|麻醉酒|麻醉枪|麻醉药|麻醉乙醚|马加爵|买卖枪|买卖枪支|卖淫|毛片|毛爷爷复活|毛一鲜|毛泽东|毛泽东复活|毛主席|毛主席复活|玫瑰园|美国凯德|美国之音|美少妇的哀羞|美少女|美腿|猛料|蒙汗|蒙汗药|蒙汗药粉|梦中的女孩|迷昏药|迷魂药|迷奸|迷奸药|迷歼药|迷药|蜜洞|灭绝罪|民警当副院长|民为法执|民运|民运分子|明慧|模拟机|南京大屠杀|南京大学法学院|嫩穴|你敢上吗|牛皮弹带|奴事件|奴役童工|女死囚|女优|女友坊|拍肩神药|牌九|盘古乐队|喷尿|喷沙枪|喷雾型迷药|喷血|嫖娼|平码|破处|欺骗股民|气枪|汽车爆炸案|千岛湖之旅|枪决女犯|枪决现场|枪模|枪淫少妇|枪油|枪支|枪支弹药|枪支等违禁品|强奸|强效失意药|强硬|强硬发言|窃听|窃听器|窃听器材|亲共行动|秦青的幸福生活|清华网管|情色|情色贴图|情色图片|情色文学|情色小说|情色影片|情欲|求救遭拒|群发|群交|绕过封锁|热辣|人民报|人权圣火|仁寿警方|日本小泉|蹂躏|肉棍|乳房|赛马会|三挫仑|三级|三级片|三角牌子弹|三句硬话|三肖|三唑仑|色空寺|色情|色情服务|色情网站|色情小电影|色友|色诱|色欲|山不过来|山西黑砖窑|山西洪洞|汕頭頻傳擄童割器官|汕尾事件|上海交警|少儿勿入|少妇白洁|少年阿宾|少女高潮|舌战法庭|涉毒|身份证生成|身份证生成器|神雕外传之郭襄|沈阳公安|升达|升达毕业证|生成身份证|声色场所|绳虐|失身|十景缎|十六次|十六大|十七大|手机复制|手机窃听器|手机游戏|手机注册|手枪|手淫|兽交|售枪|双管猎枪|双鞋的故事|双性恋|睡着的武神|死亡笔记|死刑过程|死刑枪毙|四海龙女|四六级考题|四我周|松花江污染|苏东解体|他妈的|台独|台湾身份证|太王四神记|唐人|唐人电视台|逃亡艳旅|套牌|特码|天安门|天安门事件|天鹅之旅|天灭中共|天浴|天葬|童奴工|痛批政法委|偷电|偷窥|偷拍|投毒杀人|投注|透视|透视眼镜|凸点装|突破封锁|推翻|退党|外海军火出售|萬人暴|王毓彬|王子淫传|网友自拍|往事追忆录|尾行|卫星接收器|卫星遭黑客攻击|温家堡|文凭|我操|我的性启蒙老师|我周容|无界|无界浏览|无界浏览器|无码片|五奶小青|西藏暴动|西藏独立|西藏天葬|吸食|厦门PX|厦门大游行|先烈的电话|现代艳帝传奇|限制片|香港GHB水|香港马会|想不到的黑幕|销魂|小电影|小口径步枪|小平|小泉恶搞|小穴|校花沉沦记|邪恶的党|写真|泄题|蟹骰|新疆独立|新疆分裂主义|新生网|新诗年鉴|新唐人|信号拦截器|星光伴我淫|性爱|性爱电影|性爱教育|性爱日记|性爱文学|性感|性高潮|性交|性教官|性奴|性奴营|性虐待|性事|性学教授|性游戏|性欲|徐和柴学友|学生暴动|学生与警察|学运|学运分子|血腥图片|艳舞|杨林|杨元元|摇头丸|遥想当年春衫薄|夜激情|一安士海洛|一波中特|一卡多号|一码中特|一四我|一肖中特|一夜情|倚天屠龙别记|阴唇|阴蒂|阴户|阴茎|阴毛|淫秽词语|淫间道|淫乱|淫靡|淫魔舞会|淫情女教师|淫兽学园|淫术炼金士|淫水|隐私|赢钱|油田总部|游行|有码片|幼交|诱惑视频|诱奸|玉蒲团|欲望。|愈快乐愈堕落|援交|远程偷拍|云雨图|遭警察殴打|泽东|泽民|曾道人|张大权|张小平|赵紫阳|趙紫陽|这年头就这样|针对台湾|针孔摄像机|侦探设备|真善忍|镇压学生|政府|支持台湾|智能H3|智能Ｈ３|中共|中共当局|中共独枭|中共监狱|中共警察|中共流氓特务|中共流氓政府|中共流氓政权|中共媒体|中共迫害|中共特务组织|中共网特|中共政权|中共专制|中国共产党亡|中国军用运输机|中国人都在上的四大当|中央军委|中原油田|重题工|周容重|朱蒙|朱镕基|专项整治|专制|转法轮|子女任职名单|紫阳|自杀手册|自杀指南|自慰|自由门|自由亚州|自制枪|自制手枪|总统的讲话|走光|阻无通畅|最牛钉子户|最淫官员|作弊器|做爱|做鸡|非法集资|资金池|诈骗|骗子|骗局|庞氏骗局|自融|汽枪|气抢|汽抢|货到付款|qq|代办|银行卡|汽狗|秃鹰|仿真|做证件|做原子弹|做人不能太cctv了|做爱小|唑仑|作硝化甘|作各种证|作爱|左转是政|左翼联盟|昨日重现|尊爵粉|醉乙醚|醉迷药|醉钢枪|最牛公安|组织集体|阻止中华人民共和国统|足球玩法|足球投注|租售|总书记|总会美女|宗教压迫|自主择业|自制|自由运动|自由亚洲|自由亚|自由写作奖|自由网|自由时报|自由圣|自由民主论坛|自由|自已的故事|自慰用|自拍|自己找枪|自sha|自fen|字牌汽|梓健特药|子宫|资料泄|资金周转|资金短缺|资格證|姿不对死|咨讯紫阳|咨讯|着涛哥|着护士的胸|准现房|装消音|装枪套|装弹甲|装b|赚钱资料|转是政府|专制政权|专业助|专业调查|专业代写|专业代理|专业代|专业办理|抓胸|助考网|助考|住英国房|主席像|主席李世民|主席画像|主席忏|主权|诸世纪|朱总理|朱之鑫|朱云来|朱维群|朱瑟里诺|朱镕鸡|朱溶剂|朱容基|朱容鸡|朱明国|朱琳|朱金基|朱嘉明|朱海仑|朱德|朱丹|朱成虎|朱保成|昼将近|宙最高法|周正毅|周永康|周英|周旋|周小川|周天法|周生贤|周强|周年庆|周刊纪事|周恩来自责|周恩来后悔|周恩来忏悔|州三箭|州大批贪|州惨案|重新评定|众像羔|种学历证|种公务员|钟山风雨论坛|中印边界谈判结果|中医药局|中央政治局|中央召开北戴河会议决定十八大高层人事|中央团系|中央社|中央日报|中央派系斗争|中央领导|中央zf|中央|中宣部|中石化说亏损|中日没有不友好的|中年美妇|中南海权力斗争|中南海秘闻|中南海惊现东宫小朝廷|中南海高层权利斗争|中南海斗争|中南海波澜起伏|中南海|中纪委|中华真实报道|中华养生益智功|中华时事|中华人民正邪|中华人民实话实说|中华联邦|中华局域网|中华讲清|中华帝国|中华大众|中华大地思考|中國當局|中国猪|中国政治新星|中国政坛新星中的四大天王|中国政坛“清华帮”盛极而衰|中国真实内容|中国在统一问题上的投降主义|中国移动通信|中国移动|中国新民党|中国问题论坛|中国威胁论|中国网通|中国太子|中国时报|中国社会论坛|中国社会进步党|中国社会的艾滋病|中国人权|中国民主党联合总部|中国媒体十大事件|中国论坛|中国六四真相|中国联通|中国教徒|中国和平|中国孤儿院|中国共产党|中国高层人事变动解读|中国高层权力斗争|中国改革年代政治斗争|中国复兴论坛|中国复兴党|中国泛蓝联盟|中国的陷阱|中国的人权|中国当局|中国不强|中国zf|中共十八人选|中共十八人事变动|中共十八人事|中共十八热门人选|中共十八大热门人选|中共十八大前夕人事布局出手既稳又重|中共十八大前夕|中共十八大的基层选举|中共十八布局|中共权利斗争|中共权斗|中共派系力量再达成妥协|中共领导人黑幕|中共黑|中共反腐风暴大背景揭秘|中共独裁|中珙|中功|中俄密约|中俄边界新约|中俄边界|中电信|中的班禅|中朝|中办发|中gong|致胡书记的公开信|质押贷款|治军方针定调|制作证件|制证定金|制手枪|制服诱|志洪李|志不愿跟|至国家高|指纹套|指纹膜|指纹考勤|指点江山论坛|殖器护|植物冰|知道64|支持zd|證件|症腐|政治局十八|政治局常委|政治风波|政治犯|政治反对派|政治|政一府|政坛明日之星|政坛两黑马|政权|政论区|政局雾里看花|政付|政腐|政俯|政府操|政变|政百度府|政zhi|政f|郑中|郑源|郑义|郑恩宠|证一次性|证书办|证生成器|证件集团|证件办|证监会|证到付款|正义党论坛|正邪大决战|正见网|正见|正府|正法|正帮通信公司|争鸣论坛|震死他们|震其国土|震惊一个民|震惊全球|镇压|真主|真象|真相|真他妈|真实资格|真实文凭|真善美|真钱投注|真钱斗地|真理教|真、善、忍|针孔摄象|针刺死|针刺事|针刺伤|针刺案|针刺|这不是天灾|折上折|赵晓微|赵宪庚|赵铁锤|赵品潞|赵南|赵乐际|赵克石|赵洪祝|赵海青|赵爱明|找政府评理|找政法委副|找援交|找枪手|找个妈妈|找个爸爸|招妓|招鸡|章沁生|章虹|张左己|张中伟|张志新|张志清|张志国|张昭富|张云川|张玉台|张又侠|张毅|张裔炯|张阳|张研农|张轩|张筱雨|张晓平|张晓刚|张小洋|张先玲|张文岳|张伟国|张万年|张铁健|张仕波|张汝成|张荣坤|张庆伟|张庆黎|张平|张培莉|张林|张连珍|张立昌|张开双腿|张军|张杰|张健|张建平|张纪南|张基|张惠新|张宏堡|张海阳|张国清|张耕|张高丽|张钢|张定发|张德江|张丹红|张岱梨|张春贤|张春桥|张成寅|张博涵|张伯笠|张宝顺|占领台湾|翟小衡|翟渠|炸药制|炸药配方|炸药配|炸药的制|炸药|炸学校|炸立交|炸广州|炸弹遥控|炸弹配方|炸弹教|炸弹|炸yao|炸dan|曾荫权|曾宪梓|曾庆红|曾培炎|泽d|择油录|择民|则民|造反|遭武警|遭警察|遭到警|遭便衣|葬独|臧人|臧独|昝爱宗|在线播放|韵徐娘|晕倒型|援助交际|援藏|袁腾飞|袁荣祥|袁家军|袁贵仁|袁纯清|原子能机构|原子弹|原装弹|原音铃声|原一九五七|原味内衣|园血案|园凶杀|园砍杀|园发生砍|园惨案|鸳鸯洗|冤民大|欲仙欲死|欲女|欲火|育部女官|玉穴|玉乳|玉女心经|语音|宇宙真理|宇明网|与狗性|舆论反制|舆论|愚民政|愚民同|娱乐透视|俞正声|俞杰|余远辉|余英时|余欣荣|于幼军|于起龙|于浩成|于大海|幼女|幼男|幼齿类|右转是政|有奶不一|有华龙|游戏管理员|游龙商务联盟|游精佑|游xing|油行|邮政局|由喜贵|幽谷三|优化官员|用手枪|用刀横向切腹|咏妓|婴儿汤|婴儿命|英雄纪念碑|英国金融时报|应子弹|应召|印章|印尼伊斯兰祈祷团|印尼屠华|印尼事件|隐形喷剂|隐形耳机|隐形耳|隐瞒地震|引起暴动|尹方明|银行联合管理局|淫照|淫液|淫样|淫穴|淫亵|淫威|淫娃|淫书|淫兽学|淫兽|淫声浪语|淫色|淫騷妹|淫肉|淫情女|淫情|淫妻|淫虐|淫女|淫母|淫魔舞|淫魔|淫糜|淫媚|淫浪|淫教师|淫叫|淫贱|淫河|淫妇|淫电影|淫荡|淫虫|陰戶|陰道|陰唇|阴精|阴间来电|阴核|阴阜|阴道|阴部|阴b|益受贿|益关注组|易达网络卡|异议人士|蚁力神|遗情书|移民|移动集团|移动公司|移3动|胰岛素样生长因子|伊斯兰运动|伊斯兰|伊力哈木|伊拉克|一中一台|一折起|一夜欢|一小撮别|一位老同志的谈话|一梯两户|一丝不挂|一切证件|一平|一码|一国两制|一贯道|一党专制|一党专政|一党执政|一部分人因年龄或健康原因将不得不退下去|一本道|一ye情|液体炸|夜勤病栋|叶青纯|叶剑英|叶冬松|耶和华|耀邦|要泄了|要说法|要射了|要射精了|要人权|姚增科|姚月谦|姚明进去|样板间|样板房|恙虫病|洋房|杨周|杨月清|杨衍银|杨巍|杨松|杨思敏|杨树宽|杨士秋|杨利伟|杨利民|杨晶|杨洁篪|杨建亭|杨建利|杨佳|杨焕宁|杨怀安|杨刚|杨传堂|杨传升|杨崇汇|杨j|阳具|殃视|央视内部晚会|燕南评论|艳情小说|颜射|颜色革命|阎明复|盐酸曲|言论罪|言被劳教|严晓玲|严家祺|严家其|阉割|焉荣竹|烟感器|烟草局|亚洲自由之声|亚洲周刊|亚情|雅虎香港|鸦片|压迫|丫与王益|逊克农场26队|寻找林昭的灵魂|血洗京城|血书|雪山狮子旗|學生妹|学位證|学生妹|学骚乱|学潮|学百度潮|学chao|穴图|穴口|穴海|选国家主席|许云昭|许其亮|许家屯|许达哲|徐玉元|徐一天|徐天亮|徐水良|徐守|徐绍史|徐明|徐乐江|徐匡迪|徐敬业|徐建国|徐光春|徐粉林|徐才厚|徐斌|徐邦秦|雄烯二醇|胸主席|胸推|性息|性推广歌|性虐|性技巧|性饥渴|性虎|性感诱惑|性感妖娆|性感少|性福情|性伴侣|性爱日|幸运码|姓忽悠|型手枪|形透视镜|形式主义|邢铮|邢元敏|行长王益|星亚网络影视公司|星相|星上门|星岛日报|兴中心幼|兴奋剂|信用危机|信用卡提现|信用卡空卡|信用卡|信息产业部|信接收器|信号枪|信访专班|信访局|信访|新语丝|新型毒品|新闻封锁|新闻办|新唐人电视台|新势力|新金瓶|新疆限|新疆骚乱|新疆叛|新疆独|新建户|新华通论坛|新华社|新华内情|新华举报|新观察论坛|新党|辛灝年|辛灏年|谢中之|谢选骏|谢旭人|谢小庆|谢和平|谢长廷|泄漏的内|写字楼|写两会|邪党|协警|协晃悠|校骚乱|校鸡|肖亚庆|肖强|肖捷|肖钢|肖爱玲|小泽一郎|小旺铺|小灵通|小户|小额贷款|小逼|小xue|小6灵通|销售热线|硝化甘|硝铵|详情请进入|香港总彩|香港总部|香港一类|香港明报|香港论坛|相自首|相奸|陷害罪|陷害案|宪政|宪章|宪法法院|限制言|限量|线透视镜|现金投注|现房|现大地震|鲜族|先天健康法|先烈纷纷|夏川纯|下载速度|下体|下贱|下法轮|喜贪赃|洗澡死|洗脑班|洗脑|袭警|席指着护|席临终前|席复活|习仲勋|习太子|习明泽|习晋平|习进平|习近平|稀藏|硒藏|奚国华|希葬|希脏|希特勒|希拉克|希藏|吸精|西葬|西脏|西奘|西臧|西山会议|西服进去|西独|西藏限|西藏论坛|西藏独|西布曲明|西z|雾型迷|务员考试|务员答案|舞女|武警已增|武警殴|武警暴|武吉海|武侯祠|午夜极|午夜电|五月天|五套功法|五套功|五毛们|五毛党|五出三进|五不争鸣论坛|五不|吾尔开希|吾尔开西|吾尔|吴镇南|吴毓萍|吴玉良|吴仪|吴学璨|吴学灿|吴新雄|吴双战|吴胜利|吴仁华|吴宏达|吴弘达|吴官正|吴方城|吴定富|吴邦国|吴百益|吴爱|无修正|无网界浏览|无网界|无码专|无码|无界网络|无官正|无抵押贷款|无耻语录|无耻|无帮国|乌蝇水|卧艹|卧槽|我日你|我就色|我搞台独|我干|我的西域|我草|瓮安事件|瓮安|翁安|闻封锁|闻被控制|纹了毛|文做|文学城|文物局|文强|文凭证|文化大革命|文化部|文革|瘟疫爆发|瘟假饱|瘟加饱|溫家寶|温总理|温云松|温元凯|温影帝|温如春|温切斯特|温家宝|温加保|温加饱|温加宝|温宝宝|温x|温jia宝|温jb|魏新生|魏礼群|魏京生|魏家福|魏凤和|喂奶门|谓的和谐|尉健行|位的qq|卫生部|卫留成|委坐船|伪火|维吾|维权谈|维权人|维权基|维权|维基百科|维汉员|维汉|维多利亚公园|违禁品|围攻上海|围攻警|为了忘却的纪念|微型摄像|威胁论|威而柔|旺铺|往事并不如烟|网赚队伍|网通|网特|网上赚钱|网评员|网民诬|网民获刑|网民案|网络评论员|网络封锁|网管|网关|王忠民|王志刚|王正伟|王正福|王振华|王兆国|王玉普|王勇|王毅|王益案|王冶坪|王学军|王旭东|王秀丽|王新宪|王晓初|王宪魁|王侠|王喜斌|王希哲|王伟光|王伟|王维民|王维林|王为璐|王万宾|王通智|王太华|王寿祥|王胜俊|王三运|王若望|王润生|王瑞林|王儒林|王荣|王千源|王岐山|王明方|王洛林|王立英|王立军|王力雄|王莉莉|王乐泉|王俊莲|王军涛|王进东|王金山|王建平|王建军|王家瑞|王华元|王华庆|王沪宁|王沪|王鸿举|王洪章|王和民|王涵万|王国生|王光亚|王冠中|王刚|王辅臣|王奉友|王东明|王丹|王传平|王超华|王策|王炳章|王宝森|亡党亡国|亡党|汪兆钧|汪洋|汪岷|汪梦飞|汪东兴|万晓东|万维读者论坛|万润南|万人骚动|万人大签名|万能钥匙|万里大造林|晚年周恩来|完全自杀手册|湾台|湾版假|外围赌球|外围|外透视镜|外蒙|外交与方略|外交论坛|外交部|外汇局|袜按摩|瓦斯手|脱衣艳|脱内裤|脱裤门|脱光|退dang|推油按|推油|推翻社会主义制度|推背图|推bei图|团派政治明星|团派|土共|土g|屠杀|屠光绍|涂运普|突破网路|突厥斯坦|秃鹰汽|透支|透视仪|透视药|透视器|透视扑|透视镜|透视功能|投公|头双管|偷偷贪|偷听器|偷肃贪|偷欢|偷電器|统战|统一台湾|统一教|统计局|统独论坛|统独|童增|童屹|童世平|同胞书|通信维权|通钢总经|庭审直播|庭保养|铁凝|铁道部|舔阴|舔脚|田修思|田停工|田田桑|田力普|田成平|田罢工|天推广歌|天怒|天皇|天府广场集会|天府广场|天朝特|天按门|天安门一代|天安门屠杀|天安门母亲|天安门录影带|替人体|替考试|替考|体位|体透视镜|体健貌端|体奸|提前预测|腾讯客服电话|特讯|特上门|特卖会|特价|特惠房|特贡|特共|特供|特工资|特等奖|套牌车|套弄|讨说法|讨回工资|讨伐中宣部|讨伐|陶驷驹|涛一样胡|涛共产|唐捷|唐家璇|唐柏桥|汤涛|汤加丽|汤光中|探测狗|坦克压大学生|檀鑫|谭作人|摊牌要权|贪污腐败|贪污犯|贪官也辛|贪官|泰州幼|泰兴镇中|泰兴幼|泰晤士报|太子党|太王四神|台完|台湾自由联盟|台湾猪|台湾政论区|台湾问题|台湾青年独立联盟|台湾联盟|台湾建国运动组织|台湾国|台湾狗|台湾共和国|台湾共合国|台湾独立|台湾独|台湾|台弯|台盟|台军|台海战争|台海问题|台海危机|台海统一|台海局势|台海大战|台毒|台办|台百度湾|台wan|台du|台[*]湾|蹋纳税|塔利班|他们嫌我挡了城市的道路|孙忠同|孙政才|孙晓群|孙思敬|孙金龙|孙建国|孙家正|孙大发|孙春兰|孙宝树|酸羟亚胺|粟戎生|宿命论|速取证|速代办|素人|素女心|诉讼集团|酥痒|苏贞昌|苏晓康|苏树林|苏士亮|苏绍智|苏荣|苏家屯集|苏家屯|送qb|宋祖英|宋育英|宋秀岩|宋书元|宋平一句话|宋平顺|宋平|宋楚瑜|宋爱荣|宋xx|松岛枫|四小码|四事件|四联航空|四级答案|四海帮|四风|四二六社论|四大扯个|四川独立|四川独|四博会|死要见毛|死全家|死法分布|死逼|斯诺|私人侦探|私家侦探|私房写真|司徒华|司马义·铁力瓦尔地|司马璐|司马晋|司法黑|司长期有|丝足按|丝诱|丝袜网|丝袜妹|丝袜美|丝袜恋|丝袜保|丝袜|丝情侣|丝护士|水阎王|水去车仑|水扁|氵去车仑工力|氵去车仑|氵去|谁是新中国|爽死我了|爽片|双臀|双十节|双规|双管平|双管立|刷卡消费|刷卡|数据中国|术牌具|熟女|熟母|熟妇|舒晓琴|书办理|售左轮|售子弹|售一元硬|售信用|售五四|售手枪|售肾|售三棱|售热武|售枪支|售冒名|售麻醉|售氯胺|售楼|售猎枪|售军用|售健卫|售假币|售火药|售虎头|售狗子|售防身|售弹簧刀|售单管|售纯度|售步枪|兽欲|兽性|兽奸|首付|守所死法|扌由插|手槍|手木仓|手拉鸡|手机追|手机窃|手机铃声下载|手机铃声|手机监|手机跟|手狗|手答案|手变牌|收容所|收货|收复台湾|释欲|是躲猫|试题答案|试卷|视频来源|视解密|事实独立|式粉推|示威|示wei|世界通|世界日报|世界经济导报|世纪中国基金会|世华商务|士康事件|驶你母|驶你老母|使馆|史莲喜|食堂涨价|食捻屎|食精|实学历文|实体娃|实毕业证|时事论坛|时事参考|时代论坛|石宗源|石肖|石戈|石大华|十七大幕|十类人不|十个预言|十大穷人|十大禁|十大谎|十八庆红|十八年|十八届|十八换血|十八等|十八大政治局|十八大预测|十八大未来|十八大人事调整|十八大人事安排意见|十八大人事|十八大权力变更|十八大|十7大|狮子旗|师父|失意药|失身水|尸博|盛雪辛灏年|盛雪|盛行在舞|盛华仁|圣战组织|圣战不息|圣战|省政府大门集合|省委大门集合|省市换班第五代冒起|生肖中特|生肖|生态区|生当作人杰之昨日重现|生踩踏|生被砍|审计署|沈跃跃|沈阳军区|沈彤|沈素琍|沈浩波|沈德咏|神韵艺术|神通加持法|神七假|神7州行|深圳红岭|深圳国领|深喉冰|呻吟|申维辰|涉嫌抄袭|射颜|射网枪|射爽|射精|社会主义灭亡|社会主义|社会黑暗|社保基金会|邵琪伟|邵明立|邵家健|少修正|少妇|韶关旭|韶关玩|韶关斗|烧瓶的|烧公安局|尚勇|尚福林|上中央|上门激|上海市劳动和社会保障局违规使用社保资金|上海垮台|上海孤儿院|上海帮|上访|商业楼|商务领航|商务部|商圈|商铺|善恶有报|汕尾|煽动群众|煽动不明|山涉黑|煞笔|煞逼|傻比|傻逼|傻b|刹笔|沙比|杀指南|杀警|杀毙|杀b|色小说|色视频|色色|色区|色盟|色妹妹|色猫|色界|色电影|色逼|色b|扫了爷爷|骚嘴|骚穴|骚水|骚女|骚浪|骚妇|骚比|骚逼|散弹枪|三唑|三硝基甲苯|三网友|三水法轮|三去车仑|三秒倒|三股势力|三个代表|三个呆婊|三挫|赛后骚|萨拉托加|萨达姆|瑞士金融大学|软弱的国|入联|入耳关|乳头|乳交|乳沟|乳爆|如何推翻中共|如厕死|如6意通|肉欲|肉穴|肉具|肉茎|肉缝|肉洞|肉唇|肉逼|肉棒|揉乳|柔胸粉|日你妈|日烂|日逼|日本万岁|任于斯国|任亚平|认牌绝|忍无可忍|仁青加|仁吉旺姆|人质|人真钱|人渣|人在云上|人员变动|人员安排|人游行|人体炸弹|人体艺|人兽|人事接班|人事布局出手既稳又重|人事变动|人肉炸弹|人拳|人权律|人权|人妻|人木又|人民之声论坛|人民之声|人民真实报告|人民真实报道|人民真实|人民日报|人民内情真相|人民大众时事参考|人民大众|人民大会堂|人民报讯|人类灭亡进程表|人弹|人quan|热站政论网|热线|热比娅|惹火身材|惹的国|燃烧瓶|燃烧辅助工具|燃烧弹|群体性事件|群体性事|群体事件|群体灭绝|群起抗暴|群奸暴|裙中性运动|瘸腿帮|拳交|权威主义国家的合法性理论|权贵集团|权柄|全真证|全哲洙|全裸|全家死绝|全家死光|全家不得好死|全集在线|全户型|全国两会|全1球通|去中央|娶韩国|屈万祥|区的雷人|求购|邱学强|琼花问|穷人|穷 人|请愿|请示威|请命|请集会|请点击进入|氰化钠|氰化钾|晴宜|情自拍|情视频|情趣用品|情妹妹|情聊天室|清真|清华帮|清海无上师|清純壆|清纯|清除负面|清仓|清zhen|轻舟快讯|氢弹手|青天白日旗|青天白日|勤捞致|禽流感了|秦银河|秦晋|秦光荣|钦定接班人|钦本立|亲日|亲民党|亲美|切听器|乔石|抢其火炬|抢粮记|抢购|强制捐款|强制拆除|强卫|强权政府|强奸处女|强暴|强jian|枪子弹|枪械制|枪械|枪销售|枪手网|枪手队|枪货到|枪的制|枪的结|枪的分|枪的参|枪出售|欠干|潜在用户|钱运录|钱三字经|钱其琛|钱国梁|钱达|前凸后翘|铅弹|氣槍|气狗|启蒙派|骑单车出|奇淫散|奇迹的黄|齐墨|祁建|期货配|七大军区|普通嘌|普萘洛尔|普京|仆不怕饮|剖腹一刀五千几|迫害|泼尼松|平小邓|平米|平近习|平叫到床|平反|平惨案|品香堂|嫖鸡|嫖俄罗|票据|屁眼|屁股|皮箱炸弹|彭清华|彭枫|喷精|配有消|陪考枪|砲友|炮友|炮的小蜜|盘古|潘云鹤|潘国平|牌技网|牌分析|拍肩型|鸥之歌|欧泽高|欧广源|诺查丹玛斯|诺贝尔和平奖|女優|女上门|女任职名|女人和狗|女技师|女激情|女被人家搞|怒的志愿|努尔白克力|浓精|纽约时报|纽崔莱七折|妞上门|聂卫国|聂树斌|捏弄|娘西皮|娘两腿之间|年轻高知团系接班|年福纯|拟涛哥|你他妈|你说我说论坛|你吗b|你麻痹|你妈的|你的西域|你办事我放心|倪育贤|泥马之歌|尼可刹米|嫩阴|嫩女|嫩逼|闹独立|南联盟|南京军区|南街村|南华早报|南大自由论坛|南大论坛|南充针|男奴|男公关|奈美|奶子|纳米比亚|内射|内幕|内裤|穆斯林|穆罕穆德|穆罕默德|慕歌|幕前戲|幕没有不|木子论坛|木犀地|木齐针|木牛流马的污染比汽车飞机大|木仓|母乳家|母奸|默罕默德|莫伟强|莫达非尼|末世论|摩小姐|摩门教|模型枪|摸胸|摸奶|摸nai门|铭记印尼|明心|明斯克号|明慧周报|明慧网|名宅|名师|名山县|敏主|抿主|闵维方|民族问题|民族矛盾|民主墙|民主女神|民主潮|民主|民竹|民猪|民珠|民政部|民阵|民泽江|民运人士|民意论坛|民意|民一主|民联|民抗议|民九亿商|民进党|民航局|民国|民告官|民储害|灭亡中国|灭共|苗圩|缅甸|免费索取|免费使用|免费二级域名|免费订购热线|绵恒异议人士|绵恒|蜜液|蜜穴|密穴|秘书长|秘唇|谜奸药|迷藥|迷情药|迷情水|迷魂藥|迷魂香|迷昏藥|迷昏口|迷幻藥|迷幻药|迷幻型|咪咪|梦网洪志|孟学农|孟令伟|孟建柱|蒙古分裂分子|蒙古独立|蒙古独|蒙独|猛男|氓培训|門服務|门保健|门按摩|门安天|媚外|妹上门|妹按摩|美幼|美艳少妇|美穴|美少妇|美沙酮|美乳|美联社|美利坚|美国佬|美国广播公司|美国参考|美逼|每周一死|媒体封锁|梅克保|毛贼东|毛泽东2|毛泽东123|毛相|毛太祖|毛zx|毛zd|猫贼洞|猫泽东|猫则东|猫眼工具|忙爱国|漫步丝|满洲第三帝国|满狗|瞒报|卖自考|卖银行卡|卖国唐捷|卖国|卖房|卖发票|卖地财政|卖车|麦当劳被砸|买一送一|买小车|买房|买二送一|买春|吗啡|骂四川|玛雅网|马志鹏|马英九|马晓天|马馼|马时敏|马三家|马强|马良骏|马勒|马克思|马恺|马凯|马会|马大维|马飚|蟆叫专家|麻醉藥|麻醉槍|麻醉狗|麻将透|麻黄草|麻果丸|麻果配|麻痹的|妈了个逼|妈逼|落霞缀|骆琳|骆惠宁|裸舞视|裸陪|裸露|裸聊网|罗志军|罗正富|罗斯小姐|罗清泉|罗蒙马格赛基金会|罗礼诗|罗箭|罗干|罗保铭|论文代|轮子功|轮手枪|轮功|轮法功|轮大|轮操|轮暴|伦理毛|伦理电影|伦理大|伦功|抡功|亂倫|乱伦小|乱伦类|乱交|乱奸|氯噻嗪|氯胺酮|绿色雨|绿色环保手机|吕祖善|吕秀莲|吕京花|路甬祥|鹿心社|陆委会|陆同修|陆肆|陆四|陆浩|陆封锁|陆兵|卢展工|卢越刚|露b|漏乳|楼盘|楼继伟|娄义|隆手指|龙新民|龙小霞|龙湾事件|龙卷风|龙虎豹宋书元|龙虎豹|六月联盟|六月飞雪|六位qq|六四学生运动|六四事|六四民主运动|六死|六河蟹四|六和谐四|六氟化铀|六百度四|柳树中学|柳斌杰|流淫|流血事|流血冲突|刘志军|刘振亚|刘振起|刘振来|刘云山|刘粤军|刘源|刘玉亭|刘玉浦|刘永治|刘永清|刘永川|刘延东|刘亚洲|刘学普|刘晓竹|刘晓榕|刘晓凯|刘晓江|刘晓波|刘文瑜|刘文胜|刘伟平|刘伟|刘士贤司马晋|刘士贤|刘石泉|刘少奇|刘山青|刘青|刘千石|刘淇|刘奇葆热门人选|刘奇葆|刘鹏|刘明康|刘凯中|刘俊国|刘京|刘建华|刘家义|刘慧|刘华清|刘国凯|刘刚|刘冬冬|刘春良|刘成军|刘宾雁|刘宾深|令计划|令狐计划|令狐安|领土拿|领事馆|领导干部吃王八|零八奥运艰|铃声下载|铃声|铃木麻|凌辱|凌锋|灵动卡|林左鸣|林幼芳|林业局|林炎志|林文漪|林树森|林慎立|林樵清|林明月|林军|林黄菊|林长盛|林彪|林保华|獵槍|猎槍|猎枪销|猎好帮手|劣等民族|列确|列宁|了件渔袍|廖锡龙|廖晖|聊斋艳|聊性|聊视频|两限房|两会又三|两会新闻|两会代|两会报道|两会|两个中国|两岸战争|两岸三地论坛|两岸关系|两岸才子|粮荒|梁擎墩|梁光烈|炼功|炼大法|聯繫電|廉政大论坛|联总之声传单|联总之声|联总这声传单|联总|联通商务通|联通贵宾卡|联通|联名上书|联合行动|联合起诉最高人民法院|联合国|联大|联4通|联 通 贵宾 卡|连准|连战|连锁加盟|连胜德|连发手|栗智|利他林|丽媛离|历史的伤口|力月西|力骗中央|理做帐报|理证件|理想信念斗争|理是影帝|理各种证|里鹏|李总统|李总理|李志绥|李兆焯|李幛喆|李月月鸟|李月鸟|李源潮|李玉妹|李玉赋|李愚蠢|李咏曰|李毅中|李延芝|李学勇|李学举|李小雪|李小鹏|李小朋|李小琳|李向东|李先念|李熙|李希|李文斌|李旺阳|李铁映|李天羽|李四光预测|李树菲|李淑娴|李适时|李世明|李盛霖|李少民|李三共志|李瑞环|李荣融|李契克|李买富|李禄|李录|李立国|李老师|李岚清|李兰菊|李克强接班人|李克强第五代领导人|李克强|李克|李康|李景田|李金章|李金城|李建国|李继耐|李纪恒|李鸿忠|李洪宽|李洪峰|李宏治|李红痔|李汉柏|李海峰|李刚|李法泉|李登辉|李大师|李从军|李春城|李崇禧|李成玉|李长江|李长春|李长才|李斌|李安东|李peng|李 洪 志|黎安友|类准确答|雷人女官|雷管|雷春美|老习|老人政治|老毛子|老毛|老江|老共|老j|浪穴|浪女|浪漫邂逅|浪叫|浪妇|浪逼|狼友|狼全部跪|蓝丝带|蓝光|兰州军区|赖达|来京上访|来复枪|来福枪|来福猎|啦沙|啦萨|啦撒|拉萨|拉姆斯菲尔德|拉开水晶|拉登说|拉丹|拉sa|矿难不公|邝锦文|狂插|狂操|款到发货|快速办|快感|跨世纪的良心犯|裤袜|骷髅死|口淫|口蹄疫|口手枪|口射|口活|口爆|控制媒|控诉世博|恐怖主义|恐怖份子|恐怖分子|孔摄像|孔丹|空和雅典|客户端非法字符|刻章办|刻章|克透视|克千术|克林顿|克劳塞维茨|克分析|可燃物|磕彰|考中答案|考研考中|考试枪|考试联盟|考试机构|考试答案|考试保|考试包过|考设备|考前付|考前发放|考前答案|考前答|考联盟|考考邓|考机构|考后付款|考答案|抗议|抗日|亢议|康跳楼|康涛杰|康日新|康没有不|看中国|看房|砍伤儿|砍杀幼|凯旋|凯他敏|開票|開碼|开锁工具|开盘|开邓选|开苞|卡号|卡宾枪|咖啡因|军转干部|军转安置|军转|军用手|军用品|军委主席|军委|军事委员会|军事社|军品特|军刺|觉醒的中国公民日渐清楚地认识到|绝食声|据说全民|巨乳|巨奶|巨屌|举国体|菊穴|菊门|狙击枪|就要色|就去日|就爱插|救市|酒像喝汤|酒象喝汤|酒瓶门|九学|九评共产党|九评共|九—评|九-评|九码|九龙论坛|九风|九成新|九ping|九[.]评|九、评|究生答案|静坐|静zuo|境外媒体|靖志远|靖国神社|敬请忍|径步枪|警用品|警匪|警方包庇|警车雷达|警察我们是为人民服务的|警察说保|警察殴打|警察的幌|警察被|警察|景观住宅|精子射在|精子|精装|精神药品|经文|经典谎言|京要地震|京地震|进来的罪|进化不完全的生命体|进攻台湾|津地震|津大地震|金壮龙|金钟气|金正日|金振吉|金扎金|金尧如|金书波|金日成|金麟岂是池中物|金盾工程|金道铭|借腹生子|戒严|戒yan|解决台湾|解放台湾|解放军|解放tw|姐上门|姐兼职|姐服务|姐包夜|截访|结帐|结欠|街铺|揭贪难|揭批书|接班|教育部|教养院|教徒|叫自慰|叫晶晶的女孩|脚交|焦焕成|焦国标|酱猪媳|蔣彥永|蒋文兰|蒋经国|蒋介石|蒋捷连|蒋洁敏|蒋公纪念歌|蒋峰|讲事实|讲法|疆獨|僵贼民|僵贼|茳澤民|茳泽民|将则民|姜异康|姜伟新|姜建清|姜洪禄|姜大明|姜春云|江主|江猪媳|江猪|江浙闽|江浙民|江折民|江贼民|江贼|江澤民|江责民|江泽慧江泽林|江泽慧|江沢民|江则民|江系人|江戏子|江太上|江书记|江氏集团|江青|江棋生|江派|江某某|江绵康|江绵恒|江罗|江流氓|江胡内斗|江胡|江核心|江蛤蟆|江哥|江独裁|江八点|江八|江zm|江ze民|江x|江core|贱人|贱货|贱比|贱b|建设部|建国党|简易炸|简鸿章|检查部|监管局|兼职上门|兼职|奸情|奸成瘾|贾治邦|贾育台|贾廷安|贾庆林|贾庆|贾安|甲流了|甲基安非他明|甲睾酮|甲虫跳|家属被打|家元自称玉皇大帝|家一样饱|家l福|家le福|佳静安定|加拿大皇家骑警|加盟连锁|加了服|寂寞女|寂寞男|济世灵文|济南军区|纪元|记者无疆界|擠乳汁|挤乳汁|几吧|集体自杀|集体淫|集体上访|集体腐|集体打砸|集会|急需嫖|级答案|级办理|激情炮|激情妹|激情短|激情电|激流中国|绩过后付|基督教科学箴言报|基督教|基督|基地组织|基本靠吼|积克馆|鸡毛信文汇|鸡奸|机枪|机屏蔽器|机卡密|机号卫|机号定|机定位器|霍英东临终遗言|火乍|火药配方|火药|火辣|火车也疯|活不起|混蛋|浑圆豪乳|浑圆|回族|回派|回民暴|回民|回良玉|回教|回回|回复可见|黄作兴|黄兴国|黄翔|黄献中|黄树贤|黄丽满|黄康生|黄巨|黄菊|黄局|黄晶|黄建国|黄祸|黄华华|黄殿中|黄慈萍|黄冰|皇冠投注|换妻俱乐部|换届隐忧|环球证件|还看锦涛|还会吹萧|划老公|化学扫盲|化工厂爆炸|华主席|华岳时事论坛|华岳|华语世界论坛|华夏文摘快递|华夏文摘|华通时事论坛|华盛顿邮报|华人媒体|华门开|华莱士|华建敏|华惠棋|华过锋|华国锋|华国|花园|花花公子|护法|户型|互联网审查|虎头猎|虎骑|湖淫娘|湖紧掏|胡总书记|胡总|胡振民|胡泽君|胡玉敏|胡耀邦|胡晓炼|胡温|胡王八|胡书记|胡适眼|胡权利瓜分|胡平|胡派|胡惊涛|胡锦淘|胡锦滔|胡紧套|胡江内斗|胡江|胡绩伟|胡海清|胡海峰|胡的接班人|胡春华|胡x|胡j涛|胡jt|胡jintao|胡boss|后穴|后庭|后勤集团|侯德健|鸿志|虹志|荭志|紅色恐|洪志|洪哲胜|洪吟|洪传|泓志|闳志|宏志|宏法|红志|红外透视|红色恐怖|红色贵族|红满堂|红潮谎言录|弘志|衡阳万通房产|黑庄|黑手党|黑马|黑火药的|黑车|黑逼|贺卫方|贺立旗|贺国强|贺邦靖|核蛋|河蟹社会|和平请愿书|和狗做|和狗性|和狗交|何祚庥|何勇|何清涟|何平|何家栋|何峰|何德普|喝一送一|喝血社会|号屏蔽器|号码百事通|好嫩|豪宅|豪乳|豪圈钱|航天|航空售票|航空母舰|航班|捍卫社会主义共和国|汉芯造假|汉维|汉人|韩正降职副市长|韩正|韩联潮|韩国狗|韩东方|韩长赋|海洋局|海外民运|海洛因|海关总署|海访民|海luo因|哈药直销|哈批|裹本|國內美|国资委|国之母|国贼|国一九五七|国研新闻邮件|国务院|国色天香网|国庆|国姆|国母|国民根本大法|国库折|国军|国家主要部委|国家吞得|国家软弱|国家妓|国家机密|国家安全|国际投注|国际特赦|国际法院|国锋|国峰|国产av|国办发|国wu院|郭永平|郭炎炎|郭岩华|郭树清|郭声琨|郭平|郭罗基|郭金龙|郭庚茂|郭伯雄|滚圆大乳|跪真相|规模冲突|龟头|广州军区|广闻|广告代理|广告|广东五元集团|广电局|广 闻|光祖|光学真题|光复民国|贯通两极法|管制刀具|管理员|冠希|官因发帖|官也不容|官商勾|官匪|观音法门|观世音|关卓中|关闭所有论坛|乖乖粉|鼓动一些|股市圈钱|古兰经|古怪歌|购房|狗杂种|狗日的|狗屁专家|狗娘养|狗粮|狗产蛋|狗草|狗操|贡挡|共一产一党|共王储|共贪党|共青团背景|共军|共狗|共匪|共黨|共党|共铲|共产专制|共产主义的幽灵|共产王朝|共产|共惨|共残主义|共残党|共残裆|共x党|共c党|拱铲|恭喜您的号码|恭喜你的号码|供铲谠|供铲党|供铲裆|供产|攻占台湾|攻官小姐|功友|功法|公证处|公证|公寓|公投|公头|公开信胡书记空中民主墙|公开信胡|公开信|公开小姐|公开批评中央高层领导人|公检法是流氓|公馆|公关|公产党|公安网监|公安错打|公安把秩序搞乱|弓单|工自联|工商税务两条狼|工力人|工力|工程吞得|工产党|跟踪器|根达亚文明|各种发票|各类文凭|各类考试|各个银行全称|个人崇拜|个qb|蛤蟆|葛振峰|格证考试|歌功颂德|歌德|鸽派|戈扬|告中国人民解放军广大官兵书|告洋状|告全体网民书|告全国同胞书|告全国股民同胞书|告长期|搞媛交|膏药旗|高自联|高智晟|高瞻|高薪养廉|高息贷款|高武生|高文谦|高清在线|高勤荣|高强|高利贷|高丽棒子|高考黑|高就在政|高官子女|高官互调|高官|高层人事变动|港鑫華|港馬會|港料|港澳博球|港澳办|钢珠枪|钢针狗|岡本真|肛门是邻|肛门|冈本真|感扑克|干以胜|干穴|干死你|干死|干你妈|干你|改卷内幕|改号软件|改革历程|富婆给废|富民穷|傅志寰|傅怡彬|傅雯娟|傅申奇|傅锐|傅成玉|复转军人|复制地址到地址栏|复印件制|复印件生|复式|附送枪|妇销魂|付申奇|父母下岗儿下地|腐败中国|腐败|辅导班|府集中领|府包庇|福音会|福香巴|福娃頭上|福娃的預|福尔马林|符跃兰|符强|符贵|呋塞米|夫妻交换|佛展千手法|佛同修|凤凰|逢九必乱|逢八必灾|逢9必乱|逢8必灾|冯素英|冯寿淼|冯敏刚|冯东海|锋同志|疯狂掠夺大陆股民|封锁消息|封锁消|封杀|封面女郎|封从德|风雨神州论坛|风雨神州|风水宝地|粉穴|粉碎四人帮|粉嫩|焚烧中国国旗|分众|分裂中华人民共和国|分裂中国|分裂|分家在|费私服|费良勇|诽谤罪|肥逼|非典|飞扬论坛|放尿|访问链接|访民|仿真证|房折|房展|房型|房间|房峰辉|房贷给废|防身药水|防电子眼|方舟子|方迷香|方励之|范印华|范燕琼|范长龙|饭菜涨价|反社会|反日|反人类|反屏蔽|反目|反雷达测|反恐委员会|反攻大陆|反攻|反腐败论坛|反腐败|反封锁技术|反封锁|反分裂|反测速雷|反奥|反party|珐论|珐轮|珐纶|珐沦|珐囵|珐伦|珐仑|珐抡|珐[.]輪功|砝论|砝轮|砝纶|砝沦|砝囵|砝伦|砝仑|砝抡|法正乾|法正|法谪功|法谪|法院给废|法院|法一轮一功|法一轮|法西斯|法维权|法十轮十功|法论|法輪|法轮佛法|法轮佛|法纶|法沦|法囵|法伦功|法伦|法仑|法抡功|法抡|法愣|法会|法功|法车仑|法x功|法o功|法L功|法lun|法lg|法0功|法[.]轮[.]功|法[*]功|法 轮 功|筏论|筏轮|筏纶|筏沦|筏囵|筏伦|筏仑|筏抡|阀论|阀纶|阀沦|阀囵|阀伦|阀仑|阀抡|罚纶|罚沦|罚囵|罚伦|罚仑|罚抡|垡论|垡轮|垡纶|垡沦|垡囵|垡伦|垡仑|垡抡|伐论|伐轮|伐沦|伐囵|伐伦|伐仑|伐抡|乏论|乏轮|乏纶|乏沦|乏囵|乏伦|乏仑|乏抡|發票|发正念|发生关系|发生暴动|发情|发票销|发票代|发票出|发瞟|发牌绝|发论功|发论公|发论工|发论|发轮|发纶|发沦|发囵|发伦功|发伦|发仑|发抡功|发抡|发愣|发浪|发国难财|发改委|发财诗|发~票|贰拾周年|贰拾年|二注|二乙基酰胺|二奶大|二码|儿园凶|儿园杀|儿园砍|儿园惨|恩格斯|恩氟烷|恶势力插|恶势力操|恶党|俄羅斯|俄国|屙民|躲猫猫|多维|多人轮|多美康|多党|段录定|短信商务广告|短信群发|短信平台|短信截|短信广告|度假区|杜智富|杜宇新|杜学芳|杜世成|杜青林|杜冷丁|杜鹃|杜恒岩|杜德印|杜导斌|赌球网|独立中文笔会|独立台湾会|独立台湾|独立|独夫民贼|独夫|独裁政治|独裁|读不起选个学校三万起|毒蛇钻|毒豺|豆腐渣|都进中央|都当小姐|都当警|洞小口紧|动乱|动5感地带|懂文华|董万才|董建华|董贵山|東京熱|东洲|东西南北论坛|东土耳其斯坦|东突厥斯坦伊斯兰运动|东突厥斯坦伊斯兰|东突厥斯坦解放组织|东突厥斯坦|东突解放组织|东森新闻网|东森电视|东南西北论谈|东京热|东复活|东方微点|东方时空|东方红时空|东北独立|顶花心|丁子霖|丁元|丁一平|丁香社|丁关根|蝶舞按|调教|调查婚外情|钓鱼台|钓鱼岛|屌|甸果敢|电信路|电脑传讯|电鸡|电话监|电棍|点数优惠|点金|颠覆中华人民共和国政|颠覆中国政权|第一批下海经商的人富|第一夫人|第一代领导|第五代中共领导人|第五代领导新星|第五代领导人|第五代领导|第四代领导|第四代|第三代领导|第七代领导|第六代领导|第二代领导|第7代领导|第6代领导|第5代领导|第4代领导|第3代领导|第2代领导|第21集团军|第1代领导|递纸死|帝国主义|弟子|弟大物勃|地震预测|地震来得更猛烈|地震哥|地下先烈|地下钱庄|地下刊物|地下教会|地西泮|地铁十号线塌方|地塞米松|地奈德|地论坛|地产之歌|抵制|抵zhi|底制|迪里夏提|低制|低价出售|邓质方|邓玉娇|邓颖超日记|邓爷爷转|邓笑贫|邓晓平|邓小平和他的儿子|邓天生|邓榕|邓朴方|邓楠|邓可人|邓昌友|邓xp|等人手术|等人是老|等人老百|等屁民|等级證|登天梯|登陆台湾|登辉|灯草和|的同修|得财兼|盗撮|到花心|倒陈运动的最大受益人|导小商|导人最|导人的最|导叫失|导的情人|刀林荫|刀架保安|档中央|荡女|荡尽天下|荡妇|党中央|党政一把手|党章|党校安插亲信|党退|党前干劲|党禁|党后萎|党风日下|党的喉舌|党的官|党产共|党鞭|挡中央|裆中央|当局严密封锁|当官在于|当官要精|当官靠后台|当代七整|弹药配方|弹药|旦科|丹增嘉措|戴相龙|戴秉国|贷开|贷借款|贷办|代孕妈妈|代孕|代血浆|代写论|代写毕|代生孩子|代您考|代理票据|代理发票|代表烦|代表大会|代辦|代办制|代办学|代办文|代办各|代办发票|大嘴歌|大众真人真事|大中华论坛|大中国论坛|大史纪|大史记|大史|大师|大赦国际|大乳|大肉棒|大批贪官|大奶子|大麻油|大麻树脂|大麻|大陆官方|大力抽送|大降价|大家论坛|大纪元新闻网|大雞巴|大鸡巴|大红赤龙七头十角|大波|大sb|大2众卡|打针|打折机票|打砸抢|打砸办公|打压|打台湾|打死人|打死经过|打飞机专|打倒中国|鞑子|答案卫星接收机|答案提供|答案包|答an|达赖喇嘛|达毕业证|哒赖|挫仑|催情藥|催情粉|催眠水|促性腺激素|促红细胞生成素|从陈良宇倒台看中国政局走势|刺激|次通过考|纯度黄|纯度白|春夏自由论坛|春夏之交|春夏论坛|春水横溢|吹喇叭|传中共中央关于18大的人事安排意见|传说的胡曾联手是一种假象|传九促三|传单|穿透仪器|川b26931|处男|出现暴动|出售军|出售发票|出售答案|出卖|出成绩付|酬宾|仇共|仇保兴|抽着芙蓉|抽着大中|抽一插|抽插|冲凉死|冲锋枪|充气娃|持不同政见|迟万春|迟浩田|吃精|程真|程铁军|程凯|惩贪难|惩公安|城管灭|城管暴力执法|城堡|诚信通手机商城|成人游戏|成人小|成人文|成人网站|成人图|成人视|成人色情|成人论坛|成人聊|成人电|成都军区|成本价|陈左宁|陈总统|陈子明|陈至立|陈至|陈政高|陈元|陈一谘|陈一咨|陈训秋|陈宣良|陈新权|陈小同|陈相贵|陈希同|陈希|陈文清|陈同海|陈随便|陈水扁|陈绍基|陈润儿|陈破空|陈敏尔|陈蒙|陈良宇|陈雷|陈奎元|陈军|陈建国|陈冀平|陈际瓦|陈国令|陈德铭|陈存根|陈川平|陈车|陈炳基|陈炳德|陈宝生|陈s扁|车牌隐|车仑工力|车仑|车库|车臣|潮喷|潮吹|朝鲁|超越红墙|超市|超常科学|常万全|常劲|产党共|蟾蜍迁徙|柴玲|拆迁灭|察象蚂|插阴|插我|插屁屁|插你|插进|插比|插逼|插暴|插b|策没有不|测绘局|厕奴|肏死|肏你|草你丫|草你吗|艹你|曹清|曹康泰|曹建明|曹刚川|曹长青|操我|操他妈|操死|操嫂子|操你祖宗|操你全家|操你娘|操你大爷|操了嫂|操烂|操黑|操逼|藏字石|藏西|藏人|藏青会|藏民|藏獨|藏毒|藏春阁|藏暴乱|藏m|藏du|苍蝇水|苍山兰|仓井空|惨奥|参加者回忆录|蔡振华|蔡武|蔡挺|蔡继华|蔡赴朝|蔡崇国|踩踏事故|踩踏事|采花堂|财众科技|才知道只生|擦你妈|部是这样|部忙组阁|步枪|步qiang|布什|布卖淫女|布局十八大|不要沉默|不思四化|不来提不都热西提|不穿|不查全|不查都|薄一波|薄熙来|博园区伪|博讯|博会暂停|博彩娱|波推龙|兵力部署|冰在火上|冰淫传|冰火漫|冰火九重|冰火佳|冰火毒|别他吗|别墅|别梦成灰|婊子养的|婊子|辩词与梦|变牌绝|毕业證|匕首|逼奸|屄|崩盘|苯丙胺|苯巴比妥|本无码|本田|本公司担|本公司2|本[.]拉登|被中共|被指抄袭|被干|被打死|被插|被操|贝领|北省委门|北美自由论坛|北美论坛|北美讲坛|北京之春民主论坛|北京政权|北京市亿霖|北京军区|北京风波|北京当局|北京帮|北韩|北高联|北大三角|爆炸物|爆炸|爆乳|爆你菊|爆发骚|爆草|爆zha|暴政|暴淫|暴乳|暴乱|暴力袭击|暴力虐待|暴力|暴奸|暴干|暴动|鲍筒|鲍彤|鲍戈|报警|报复执法|保密室|保监会|保过答案|保钓组织|宝在甘肃修|包二奶|磅遥控器|磅解码器|谤罪获刑|绑架|帮穷人|帮忙点一下|帮忙点下|辦證|辦毕业|伴游|半刺刀|办怔|办文凭|办理资格|办理证书|办理真实|办理票据|办理各种|办理本科|办本科|版署|板楼|败培训|白志健|白皮书|白嫩|白梦|白立朴|白立忱|白军|白景富|白黄牙签|白恩培|白春礼|白痴|霸课|霸工|霸餐|罢运|罢学|罢课|罢考|罢教|罢工门|罢工|罢参|罢ke|把学生整|把邓小平|把病人整|拔出来|巴音|巴特尔|巴赫|八老|八九政治|八九学|八九年|八九民|八九|八级地震毫无预报|懊运|懊孕|奥晕|奥运会|奥运|奥孕|奥你妈的运|凹晕|暗杀|案的准确|按照马雅历法|按摩棒|按摩|安装卫星电视|安全局|安全监管|安全部|安南|安眠藥|安眠酮|安门事|安立敏|安理会|安拉|安局豪华|安局办公楼|安街逆|安魂网|安非他命|爱滋|爱液横流|爱液|爱他死|爱女人|爱国者同盟|爱国运动正名|艾斯海提·克里木拜|挨了一炮|阿旺晋美|阿拉法特|阿拉伯|阿拉|阿共|阿芙蓉|阿波罗网|阿賓|阿宾|阿扁推翻|阿扁|z东|zi杀|zhuanfalun|zhenshanren|zhengjianwang|zhengjian|zhengfu|zha药|zha弹|zhayao|zhadan|zf大楼|ze民|ze东|zemin|zedong|zang人|zangdu|y佳|yy通讯录|yuming|yuce|you行|youxing|youtube|yin荡|yang佳|yangjia|x民党|x藏|xuechao|xjp|xi藏|xizang|xinsheng|xing伴侣|xiao平|xiao77|wstaiji|wikipedia|weng安|wengan|wangce|voachinese|voa|ustibet|UP新势力|UP8新势力|unixbox|UltraSurf|txwq[.]net|txt下载|twdl|tuidang|triangleboy|triangle|tokyohot|tnt|TMD|titor|tibetalk|tibet|thegateofheavenlypeace|testosterone|tamoxifen|taiwan|taip|taidu|svdc|suicide|strychnine|Soccer01[.]com|sm女王|sm|simple|shit|shangfang|sexinsex|sex|secom|sb|safeweb|rfa|renquan|renmingbao|renminbao|QQ宠物|QQb|qingzhen|porn|playboy|piao|peacehall|paper64|nowto|nmis|NMD|narcotic|nacb|na?ve|morphine|min主|minghuinews|minghui|mdma|mayingjiu|making|ma|lihongzhi|la萨|lasa|l2590803027|k粉|KEY_TEXT|ketamine|KC网站|KC提示|KC嘉年华|KC短信|jzm|jq的来|jiuping|jing坐|jie严|jieyan|jiangdongriji|IP17908|incest|h动漫|h动画|hypermart[.]net|hujintao|huanet|hrichina|hongzhi|hmtd|hjt|heroin|hardcore|h1n1|g片|g匪|g点|g产|googleblogger|gong和|gong党|gongchandang|gfw|gc党|gcd|gay片|GameMaster|fuck your mother|fuck|Freenet|freedom|Freechina|FOCUSC|fl功|flg|fenlie|fa轮|falungong|falu|fa lun|E周刊|erythropoietin|duli|dpp大法|dpp|dl喇嘛|di制|dizhi|die|DICK|diamorphine|diacetylmorphine|Dfdz|df''d|da选|da案|dalai|Dajiyuan|Creaders|communistparty|cocain|cnd|Chinesenewsnet|chinesedemocracy|chinamz|Chinaliberal|chengdu|chairmanmao|CDMA|CCTV|CBD|cao你|cannabis|boycott|boxun|bloodisonthesquare|bjork|BJ|Bitch|bignews|benzodiazepines|bb弹|bbc中文网|ba课|bao炸|baozha|baoluan|baodong|a扁|AV|androst|anal|amateur|adult|adrenaline|a4y|a4u|9学|9评|9风|9ping|99bb|9[.]635|8的平方事件|89年春夏之交|89-64cdjp|8945212|8341部队|7大军区|75事件|7[.]310|7[.]31|6位qq|6a6[.]net|68170802|64运动|64时期|64惨案|6-4tianwang|628事件|610洗脑班|5月35|5毛党|4事件|4风|4-Jun|40万名车车主名单|4[.]25事件|3P|381929279|2o年|259o|23条|20和谐年|2006年十句最真实的谎言|2004年公共服务奖|198964|18预测|18权力布局|18禁|18届中央|18届委员|18届名单|18届常委|18届|18高官互调|18大委员名单|18大人事变动|18大的人事安排意见|18大的人事|18大|18da|17da|1717wg[.]88448[.]com|15112886328|13875448369|13725516608|13423205670|12593|12590|1259|1258|10八|10159|10086|10010|1 0 1 5 9|08宪|08xz|001工程");
            return sb.ToString();
        }

        /// <summary>
        /// 安心城市关键词过滤
        /// autor:lzh 2015-2-4
        /// </summary>
        /// <returns></returns>
        public static string keyCityWords()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("私家侦探|针孔摄象|婚外情|借腹生子|高清在线|六合彩|习近平|温家宝|薄熙来|江泽民|口交|胡锦涛|习仲勋|周小川|共产党|");
            sb.Append("李洪志|迷昏药|迷魂药|窃听器|麻醉药|摇头丸|透视眼镜|小电影|骚乱|春药|自慰|淫荡|做爱|国民党|徐才厚|周永康|六四|");
            sb.Append("法轮|傻逼|强奸|屌|干你|轮功|fa轮|屄|口爆|内射|淫|富婆|包养|出售|自制|炸弹|太子|打倒|起义|");
            sb.Append("阿扁推翻|阿宾|阿賓|挨了一炮|爱液横流|安街逆|安局办公楼|安局豪华|安门事|安眠藥|案的准确|八九民|八九学|八九政治|把病人整|");
            sb.Append("把邓小平|把学生整|罢工门|白黄牙签|败培训|办本科|办理本科|办理各种|办理票据|办理文凭|办理真实|办理证书|办理资格|办文凭|办怔|办证|");
            sb.Append("半刺刀|辦毕业|辦證|谤罪获刑|磅解码器|磅遥控器|宝在甘肃修|保过答案|报复执法|爆发骚|包小姐|北省委门|被打死|被指抄袭|被中共|本公司担|");
            sb.Append("本无码|无码|苍井空|毕业證|变牌绝|辩词与梦|冰毒|冰火毒|冰火佳|冰火九重|冰火漫|冰淫传|冰在火上|波推龙|博彩娱|博会暂停|博园区伪|不查都|");
            sb.Append("不查全|不思四化|布卖淫女|部忙组阁|部是这样|财众科技|采花堂|踩踏事|苍山兰|藏春阁|苍蝇水|藏獨|操了嫂|操嫂子|策没有不|插屁屁|察象蚂|拆迁灭|");
            sb.Append("车牌隐|成人电|成人卡通|成人聊|成人片|成人视|成人图|成人文|成人小|城管灭|惩公安|惩贪难|充气娃|冲凉死|抽着大中|抽着芙蓉|出成绩付|出售发票|");
            sb.Append("出售军|穿透仪器|春水横溢|纯度白|纯度黄|次通过考|催眠水|催情粉|催情药|催情藥|打标语|打飞机专|打死人|打砸办公|大鸡巴|大雞巴|大纪元|大揭露|");
            sb.Append("大奶子|大批贪官|大肉棒|大嘴歌|代办发票|代办各|代办文|代办学|代理发票|代理票据|到花心|电鸡|顶花心|洞小口紧|東京熱|都当小姐|对日强硬|多美康|");
            sb.Append("二奶大|夫妻交换|妇销魂|府包庇|肛交|冈本真|改号软件|攻官小姐|官商勾|乖乖粉|国家妓|和狗性|和狗交|胡紧套|胡錦濤|换妻|浑圆豪乳|激情妹|激情炮|急需嫖|");
            sb.Append("挤乳汁|奸成瘾|姐包夜|姐兼职|姐上门|精子射在|就爱插|就要色|巨乳|砍杀幼|考试枪|浪穴|亂倫|伦理片|伦理电影|裸聊网|裸舞视|麻醉藥|妹按摩|妹上门|");
            sb.Append("迷幻药|迷幻藥|迷昏口|迷昏药|迷奸药|迷情水|迷情药|内射|嫩穴|嫩阴|娘两腿之间|妞上门|浓精|女激情|女技师|女人和狗|女優|嫖鸡|丝袜妹|丝袜美|丝袜恋|丝情侣|");
            sb.Append("私房写真|透视镜|脱衣艳|性感少|性福情|性爱日|學生妹|夜激情|要射精了|要射了|蚁力神|淫情女|淫穴|淫水|用手枪|总会美女|上门|学生妹|酒店小姐|保健按摩|女郎|");
            sb.Append("洋妞|美腿|高跟鞋|大保健|兼职女|模特|鸡婆|骚|上門|按摩|美女|操|性感|找小姐");
            return sb.ToString();
        }

        /// <summary>
        /// 替换Html 如:＜link＞或 ＜／link＞
        /// </summary>
        /// <param name="label">标签名称,如:a,br,table,title等;</param>
        /// <param name="input">源字串</param>
        /// <param name="replacement">替换的目标字串</param>
        /// <returns></returns>
        public static string ReplaceHtml(string label, string input, string replacement)
        {
            string pattern = string.Format("(<{0}[^>]*>)|</[^>]*{0}>", label);

            string res = Replace(input, pattern, replacement);

            return res.Replace("&nbsp;", " ");
        }

        /// <summary>
        /// 替换HTML标签，包括标签中内容,如:＜link＞XXX内容XX ＜／link＞
        /// </summary>
        /// <param name="label">标签名称,如:a,br,table,title等;</param>
        /// <param name="input">源字串</param>
        /// <param name="replacement">替换的目标字串</param>
        /// <returns></returns>
        public static string ReplaceHtmlLabel(string label, string input, string replacement)
        {
            string pattern = string.Format("(<{0}[^>]*>).*?</[^>]*{0}>", label);

            string res = Replace(input, pattern, replacement);

            return res;
        }

        public static bool IsHtml(string input)
        {
            return Regex.IsMatch(input, "<[^>]+>");
        }
        #endregion

        #region MatchsHtml notes

        public static HtmlLinkLabel[] MatchLinks(string input)
        {
            string pattern = "<a[^>]+href=(\"(?<href>[^\"]*)\"|'(?<href>[^']*)'|(?<href>[^\\s\\n>]*))[^>]*>(?<innerText>.*?)</a>";

            MatchCollection mc = Matches(input, pattern);

            HtmlLinkLabel[] arrLinks = new HtmlLinkLabel[mc.Count];

            for (int i = 0; i < mc.Count; i++)
            {
                Match m = mc[i];

                HtmlLinkLabel link = new HtmlLinkLabel();

                link.Href = m.Groups["href"].Value;

                link.InnerText = m.Groups["innerText"].Value;

                link.LabelText = m.Value;

                arrLinks[i] = link;
            }

            return arrLinks;
        }

        #endregion
    }

    public class HtmlLinkLabel
    {
        public string LabelText = string.Empty;
        public string InnerText = string.Empty;
        public string Href = string.Empty;
    }

}
