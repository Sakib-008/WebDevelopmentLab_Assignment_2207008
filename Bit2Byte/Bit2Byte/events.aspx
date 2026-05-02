<%@ Page Title="Events" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="Bit2Byte.Events" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="page-section container">
        <h2 class="section-title">Upcoming Events</h2>
            <p>
            Below is our sample events schedule for the semester. Each event is designed to help members code,
            research, collaborate, and improve practical software skills.
        </p>

        <table class="events-table">
            <thead>
                <tr>
                    <th>Event</th>
                    <th>Date</th>
                    <th>Time</th>
                    <th>Location</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Intro to Bit2Byte</td>
                    <td>April 15, 2026</td>
                    <td>4:00 PM</td>
                    <td>Seminar Room 1</td>
                </tr>
                <tr>
                    <td>Software Research Talk</td>
                    <td>April 22, 2026</td>
                    <td>3:30 PM</td>
                    <td>Room B201</td>
                </tr>
                <tr>
                    <td>Project Sprint Session</td>
                    <td>April 30, 2026</td>
                    <td>9:00 AM</td>
                    <td>Computer Lab</td>
                </tr>
                <tr>
                    <td>Demo &amp; Feedback Meetup</td>
                    <td>May 6, 2026</td>
                    <td>5:00 PM</td>
                    <td>Student Lounge</td>
                </tr>
            </tbody>
        </table>
    </main>
</asp:Content>