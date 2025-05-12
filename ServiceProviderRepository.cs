using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;  // Added for Form class

namespace DatabaseProject
{
    public partial class ServiceProviderRepository : Form
    {
        private static string connectionString = "Data Source=LIVERPOOL\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"; // Made static

        public ServiceProviderRepository()
        {
            // Form initialization if needed
        }

        // Get all hotels owned by a specific service provider
        public static DataTable GetHotelsByServiceProvider(string providerId)
        {
            DataTable dtHotels = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT h.HotelID, h.Name, h.Capacity, h.Amenities, h.Description
                    FROM HOTEL h
                    WHERE h.ProviderID = @ProviderId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProviderId", providerId);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    try
                    {
                        connection.Open();
                        adapter.Fill(dtHotels);
                    }
                    catch (Exception ex)
                    {
                        // Log error
                        Console.WriteLine("Error retrieving hotels: " + ex.Message);
                    }
                }
            }

            return dtHotels;
        }

        // Get occupancy rate for a specific hotel
        public static DataTable GetHotelOccupancyRate(string hotelId)
        {
            DataTable dtOccupancy = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        t.TripID,
                        t.Title AS TripName,
                        t.StartDate,
                        t.EndDate,
                        COUNT(b.BookingID) AS BookingsCount,
                        SUM(b.NoOfTravelers) AS TotalGuests,
                        h.Capacity,
                        CAST(SUM(b.NoOfTravelers) * 100.0 / (CASE WHEN h.Capacity = 0 THEN 1 ELSE h.Capacity END) AS DECIMAL(5,2)) AS OccupancyRate
                    FROM TRIP t
                    JOIN TRIP_SERVICES_Renrollment tsr ON t.TripID = tsr.TripID
                    JOIN HOTEL h ON tsr.ServiceID = h.HotelID
                    LEFT JOIN BOOKING b ON t.TripID = b.TripID AND b.Status IN ('Confirmed', 'Completed')
                    WHERE h.HotelID = @HotelId
                    GROUP BY t.TripID, t.Title, t.StartDate, t.EndDate, h.Capacity
                    ORDER BY t.StartDate DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@HotelId", hotelId);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    try
                    {
                        connection.Open();
                        adapter.Fill(dtOccupancy);
                    }
                    catch (Exception ex)
                    {
                        // Log error
                        Console.WriteLine("Error retrieving hotel occupancy: " + ex.Message);
                    }
                }
            }

            return dtOccupancy;
        }

        // Get all guides owned by a specific service provider
        public static DataTable GetGuidesByServiceProvider(string providerId)
        {
            DataTable dtGuides = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                                    SELECT 
                                        u.UserID AS GuideID,
                                        CONCAT(u.FirstName, ' ', u.LastName) AS Name,
                                        sp.Rating,
                                        u.PhoneNumber,
                                        u.Email
                                    FROM SERVICE_PROVIDER sp
                                    JOIN [USER] u ON u.UserID = sp.ProviderID
                                    WHERE sp.ProviderID = @ProviderId;
                                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProviderId", providerId);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    try
                    {
                        connection.Open();
                        adapter.Fill(dtGuides);
                    }
                    catch (Exception ex)
                    {
                        // Log error
                        Console.WriteLine("Error retrieving guides: " + ex.Message);
                    }
                }
            }

            return dtGuides;
        }

        // Get ratings for a specific guide
        public static DataTable GetGuideRatings(int guideId)
        {
            DataTable dtRatings = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                        SELECT 
                            t.TripName,
                            r.RatingDate,
                            r.Rating,
                            r.Feedback
                        FROM GUIDE_RATINGS r
                        JOIN TRIP t ON t.TripID = r.TripID
                        WHERE r.GuideID = @GuideID
                        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GuideID", guideId);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    try
                    {
                        connection.Open();
                        adapter.Fill(dtRatings);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error retrieving guide ratings: " + ex.Message);
                    }
                }
            }

            return dtRatings;
        }

        public static double GetGuideAverageRating(int guideId)
        {
            double averageRating = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                        SELECT AVG(Rating * 1.0)
                        FROM GUIDE_RATINGS
                        WHERE GuideID = @GuideID
                        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GuideID", guideId);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                            averageRating = Convert.ToDouble(result);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error retrieving guide average rating: " + ex.Message);
                    }
                }
            }

            return averageRating;
        }

        // Get all transport services owned by a specific service provider
        public static DataTable GetTransportsByServiceProvider(string providerId)
        {
            DataTable dtTransports = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT tp.TransportID, tp.Specializations, tp.LicenseDetails, tp.ServiceAreas
                    FROM TRANSPORT_PROVIDER tp
                    WHERE tp.ProviderID = @ProviderId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProviderId", providerId);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    try
                    {
                        connection.Open();
                        adapter.Fill(dtTransports);
                    }
                    catch (Exception ex)
                    {
                        // Log error
                        Console.WriteLine("Error retrieving transports: " + ex.Message);
                    }
                }
            }

            return dtTransports;
        }

        // Get vehicles for a specific transport provider
        public static DataTable GetVehiclesByTransport(string transportId)
        {
            DataTable dtVehicles = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT v.VehicleID, v.Name, v.Model, v.Color
                    FROM VEHICLE v
                    JOIN TRANSPORT_PROVIDER_VEHICLE_ENROLLMENT tpve ON v.VehicleID = tpve.VehicleID
                    WHERE tpve.TransportID = @TransportId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TransportId", transportId);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    try
                    {
                        connection.Open();
                        adapter.Fill(dtVehicles);
                    }
                    catch (Exception ex)
                    {
                        // Log error
                        Console.WriteLine("Error retrieving vehicles: " + ex.Message);
                    }
                }
            }

            return dtVehicles;
        }

        // Get on-time performance for a specific transport provider
        public static DataTable GetTransportPerformance(int transportId)
        {
            DataTable dtPerformance = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
        DECLARE @TransportID INT = 3;

        WITH TransportTrips AS (
            SELECT DISTINCT 
                tp.TransportID,
                t.TripID,
                t.Title AS TripName,
                t.StartDate,
                t.EndDate
            FROM TRANSPORT_PROVIDER tp
            JOIN TRIP_SERVICES_Renrollment tsr ON tp.TransportID = tsr.ServiceID
            JOIN TRIP t ON tsr.TripID = t.TripID
            WHERE tp.TransportID = @TransportID
        ),
        TransportReviews AS (
            SELECT 
                TransportID,
                AVG(DriverRating) AS DriverRating,
                AVG(SafetyRating) AS SafetyRating,
                AVG(PunctualityRating) AS PunctualityRating
            FROM TRANSPORT_REVIEW
            WHERE TransportID = @TransportID
            GROUP BY TransportID
        )
        SELECT 
            tt.TripID,
            tt.TripName,
            tt.StartDate,
            tt.EndDate,
            CAST(
                (ISNULL(tr.DriverRating, 0) + 
                 ISNULL(tr.SafetyRating, 0) + 
                 ISNULL(tr.PunctualityRating, 0)) / 3.0 
                AS DECIMAL(4,1)
            ) AS OverallRating,
            ISNULL(tr.PunctualityRating, 0) AS PunctualityRating,
            ISNULL(tr.SafetyRating, 0) AS SafetyRating,
            ISNULL(tr.DriverRating, 0) AS DriverRating
        FROM TransportTrips tt
        LEFT JOIN TransportReviews tr 
            ON tt.TransportID = tr.TransportID 
        ORDER BY tt.StartDate DESC;
        ";

                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Parse the transportId to an integer
                        command.Parameters.AddWithValue("@TransportIdParam", Convert.ToInt32(transportId));

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        connection.Open();
                        int rowsAffected = adapter.Fill(dtPerformance);

                        // Detailed logging
                        Console.WriteLine($"Transport ID: {transportId}");
                        Console.WriteLine($"Rows Retrieved: {rowsAffected}");

                        foreach (DataRow row in dtPerformance.Rows)
                        {
                            Console.WriteLine(string.Join(", ", row.ItemArray));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception in GetTransportPerformance: {ex.Message}");
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                }
            }
            return dtPerformance;
        }

        // Get service utilization for a specific service provider
        public static DataTable GetServiceUtilization(string providerId)
        {
            DataTable dtUtilization = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        s.ServiceID,
                        s.ServiceType,
                        CASE 
                            WHEN s.ServiceType = 'Hotel' THEN (SELECT h.Name FROM HOTEL h WHERE h.HotelID = s.ServiceID)
                            WHEN s.ServiceType = 'Guide' THEN 'Guide Service #' + CAST(s.ServiceID AS VARCHAR(10))
                            WHEN s.ServiceType = 'Transport' THEN 'Transport Service #' + CAST(s.ServiceID AS VARCHAR(10))
                        END AS ServiceName,
                        COUNT(DISTINCT tsr.TripID) AS TotalTripsAssigned,
                        COUNT(DISTINCT b.BookingID) AS TotalBookings,
                        SUM(b.NoOfTravelers) AS TotalTravelers
                    FROM SERVICES s
                    LEFT JOIN TRIP_SERVICES_Renrollment tsr ON s.ServiceID = tsr.ServiceID
                    LEFT JOIN TRIP t ON tsr.TripID = t.TripID
                    LEFT JOIN BOOKING b ON t.TripID = b.TripID AND b.Status IN ('Confirmed', 'Completed')
                    WHERE 
                        (s.ServiceType = 'Hotel' AND EXISTS (SELECT 1 FROM HOTEL h WHERE h.HotelID = s.ServiceID AND h.ProviderID = @ProviderId))
                        OR (s.ServiceType = 'Guide' AND EXISTS (SELECT 1 FROM GUIDE g WHERE g.GuideID = s.ServiceID AND g.ProviderID = @ProviderId))
                        OR (s.ServiceType = 'Transport' AND EXISTS (SELECT 1 FROM TRANSPORT_PROVIDER tp WHERE tp.TransportID = s.ServiceID AND tp.ProviderID = @ProviderId))
                    GROUP BY s.ServiceID, s.ServiceType
                    ORDER BY TotalBookings DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProviderId", providerId);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    try
                    {
                        connection.Open();
                        adapter.Fill(dtUtilization);
                    }
                    catch (Exception ex)
                    {
                        // Log error
                        Console.WriteLine("Error retrieving service utilization: " + ex.Message);
                    }
                }
            }

            return dtUtilization;
        }
    }
}