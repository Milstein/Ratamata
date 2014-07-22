<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AspxABTesting.ascx.cs" Inherits="Modules_AspxCommerce_AspxABTesting_AspxABTesting" %>


<script type="text/javascript">
    //<![CDATA[   
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxABTestingLocale
        });
    });
    var aspxABTestingModulePath = '<%=AspxABTestingModulePath %>';
    //]]>
</script>

<div id="divAsxpABTesting">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <label id="lblHeading" class="sfLocale">A/B Testing</label>
            </h1>
        </div>

        <div id="divABTestingHome" class="cssABTestingHome" style="display: none;">


            
                <div class="sfButtonwrapper">
                                <label id="btnAddNewTest" class="sfAdd sfBtn icon-addnew sfLocale">New A/B Test</label>
                            </div>

                <div class="sfGridwrapper">
                    <div class="sfGridWrapperContent">
                    
                     <div class="sfFormwrapper sfTableOption"> <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                           <input type="text" id="txtSearchABTestName" class="sfTextBoxSmall" style="float: left; margin-right: 20px;" /><label id="btnSearchABTestName" class="sfSearch sfBtn icon-search sfLocale">Search</label>
                        </td>
                        
                    </tr>
                </table>
                </div>
                    <div class="loading">
                        <img id="ajaxABTestImage" src="" title="loading...." alt="loading...." class="sfLocale" />
                    </div>
                    <div class="log">
                    </div>
                  
                    <table id="gdvABTests" cellspacing="0" cellpadding="0" border="0" width="100%">
                    </table>
                
                </div>
            </div>

        </div>

        <div id="divABTestingAdd" style="display: none;">
            <div class="cssClassCommonBox Curve">
                <div class="cssClassHeader">
                    <h4>
                        <asp:Label ID="lblABTestingHead" runat="server" 
                            Text="A/B Testing Configuration :" meta:resourcekey="lblABTestingHeadResource1"></asp:Label>
                    </h4><br />
                </div>
                <div class="sfFormwrapper clearfix">
                    <div class="sfGridWrapperContent">
                        <table id="tblNewABTest" border="0" cellpadding="0" cellspacing="0">
                            <tr id="trABTestEnd" style="display: none;">
                                <td>
                                    <label id="lblABTetsEnd" class="sfFormlabel sfLocale">
                                        A/B Test :</label>
                                </td>
                                <td>
                                    <select id="ddlABTestEnd" name="ABTestEnd" class="sfListmenu">
                                        <option value="true" class="sfLocale">ON</option>
                                        <option value="false" class="sfLocale">OFF</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblABTestName" class="sfFormlabel sfLocale">
                                        Name of A/B test :</label>
                                </td>
                                <td>
                                    <input type="text" id="txtABTestName" name="ABTestName" class="sfInputbox required" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblABTestEmailNotification" class="sfFormlabel sfLocale">
                                        Email notification :</label>
                                </td>
                                <td>
                                    <select id="ddlABTestEmailNotification" name="ABTestEmailNotification" class="sfListmenu">
                                        <option value="true" class="sfLocale">ON</option>
                                        <option value="false" class="sfLocale">OFF</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblEndsOnDate" class="sfFormlabel sfLocale">
                                        Ends on date :</label>
                                </td>
                                <td>
                                    <input type="text" id="txtEndsOnDate" name="EndsOnDate" class="sfInputbox required" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblUsersInRole" class="sfFormlabel sfLocale">
                                        Users in Role :</label>
                                </td>
                                <td>
                                    <select id="ddlUsersInRole" name="UsersInRole" class="sfListmenu required error" multiple="multiple">
                                    </select>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblOriginalPage" class="sfFormlabel sfLocale">
                                        Original page :</label>
                                </td>
                                <td>
                                    <input type="text" id="txtOriginalPage" name="OriginalPage" class="sfInputbox required" onchange="AspxABTesting.OnChangeEventPageURL(this.value, this.id);" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblVariant1" class="sfFormlabel sfLocale">
                                        Variant1 :</label>
                                </td>
                                <td>
                                    <input type="text" id="txtVariant1" name="Variant1" class="sfInputbox required" onchange="AspxABTesting.OnChangeEventPageURL(this.value, this.id);" />

                                </td>
                            </tr>
                        </table>
                        <div class="sfButtonwrapper">
                        <label id="lblAddVariation" class="sfAdd sfBtn icon-addnew sfLocale">Add Variation</label>
                        </div>
                    </div>
                </div>
                <div class="sfButtonwrapper">
                    <label id="lblSaveNewABTest" class="sfSave sfBtn icon-save sfLocale">Save</label>
                    <label id="lblBackNewABTest" class="sfCancel sfBtn icon-close sfLocale">Cancel</label>
                </div>
            </div>
            <input type="hidden" id="hdnABTestID" value="0" />
        </div>

        <div class="cssClassCommonBox Curve" id="divTestDetailForm" style="display: none">
            <div class="cssClassHeader clearfix">
                <h4 class="sfFloatLeft"><span class="sfLocale">A/B Test Details:</span><span id="spanTestName"></span></h4>
            <div class="sfTabInterface clearfix">
                    <a href="#" id="ADay" onclick="return false;" class="cssClasslnkDay sfLocale">Day</a>
                    <a href="#" id="AMonth" onclick="return false;" class="cssClasslnkDay sfLocale">Month</a>
                    <a href="#" id="AYear" onclick="return false;" class="cssClasslnkDay sfLocale">Year</a>
                </div>
            </div><br />

            <div id="divShort" class="clearfix">


                <div class="sfTableOption">
                    <span class="cssClassLabel sfLocale">Visits:</span><span id="spanVisits"></span>&nbsp;&nbsp;<b></b>
                    <br />
                    <span class="cssClassLabel sfLocale">Day(s) of Data:</span><span id="spanDaysOfData"></span>&nbsp;&nbsp;<b></b>
                    <br />
                    <b><span class="cssClassLabel sfLocale">Status:</span></b>&nbsp;&nbsp;<span id="spanStatus"></span>
                    <br />


                </div>


                

            </div>
            <div class="sfFormwrapper">
            <div class="cssClassCommonBox Curve">
                <div class="sfGridwrapper">
                    <div class="sfGridWrapperContent">
                        <table id="tblTestDetails" cellspacing="0" cellpadding="0" border="0" width="100%">
                            <thead>
                                <tr class="cssClassHeading">
                                    <td class="sfLocale cssClassTDHeading">Variation</td>
                                    <td class="sfLocale cssClassTDHeading">Visit</td>
                                    <td class="sfLocale cssClassTDHeading">Conversion</td>
                                    <td class="sfLocale cssClassTDHeading">Conversion Rate</td>
                                    <td class="sfLocale cssClassTDHeading">Compare to Original</td>
                                    <td class="sfLocale cssClassTDHeading">Chances to Beat Original</td>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                        <span class="remarks"></span>
                        <input type="hidden" id="hdnABTestIDforView" value="0" />
                        <input type="hidden" id="hdnABTestNameforView" value="" />

                    </div>
                </div>
            </div>
			<div id="divABTestDetail" class="cssClassABTestDetail">
                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                    <tr>
                        <td>
                            <div id="divABTestChart">
                                <div id="divABTestVisit" class="code" style="height: 300px; width: 100%; margin: 0 auto;">
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
                <em class="sfLocale">Note: To see detail data of some portion in the above diagram you need to drag to that portion and to go full view mode double click anywhere.</em>
            </div>
            </div>
            <div class="sfButtonwrapper sftype1">
                <label id="lblCancelFromDetails" class="sfBtn icon-close sfLocale">Cancel</label>
            </div>
        </div>

    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {

        $('#AYear').addClass('active');

    });

</script>
