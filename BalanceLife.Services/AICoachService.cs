using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BalanceLife.Services.Abstraction;
using Microsoft.Extensions.Configuration;

namespace BalanceLife.Services
{
    public class AICoachService : IAICoachService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IUserGoalsService _userGoalsService;
        private readonly IMealTrackingService _mealTrackingService;
        private readonly IDashboardService _dashboardService;

        public AICoachService(
            HttpClient httpClient,
            IConfiguration configuration,
            IUserGoalsService userGoalsService,
            IMealTrackingService mealTrackingService,
            IDashboardService dashboardService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _userGoalsService = userGoalsService;
            _mealTrackingService = mealTrackingService;
            _dashboardService = dashboardService;
        }
        public async Task<string> AskAsync(
    string userId,
    string userName,
    string message)
        {
            var apiKey =
                _configuration["Groq:ApiKey"];

            _httpClient.DefaultRequestHeaders.Clear();

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    apiKey);

            var dashboard =
        await _dashboardService.GetDashboardAsync(
            userId,
            userName);

            var caloriesCurrent = dashboard.Calories.Current;
            var caloriesGoal = dashboard.Calories.Goal;

            var waterCurrent = dashboard.Water.Current;
            var waterGoal = dashboard.Water.Goal;

            var activityCurrent = dashboard.Activity.Current;
            var activityGoal = dashboard.Activity.Goal;


            var prompt = $@"
You are Balance Life AI Coach, a friendly, supportive, and professional health coach.

Your goal is to help users improve their health, nutrition, hydration, and activity habits through personalized advice.

Rules:

* Detect the user's language automatically.
* Reply in the same language as the user's message.
* If the user writes in Arabic, reply in Arabic.
* If the user writes in English, reply in English.
* Address the user by their name naturally.
* Never call the user 'User'.
* Never claim that you don't know the user's name.
* Be friendly, supportive, and motivational.
* Be realistic and honest.
* Do not overpraise poor progress.
* Do not make assumptions that are not supported by the user's statistics.
* Base all evaluations strictly on the provided numbers.
* Use a conversational tone, not a robotic report.
* Sound like a real health coach having a natural conversation.
* Keep responses concise and easy to read.
* Use emojis occasionally but do not overuse them.
* Focus on helping rather than reporting data.
* Avoid repeating all statistics unless necessary.
* Always personalize advice using the user's current progress and goals.
* Activity values represent MINUTES, not number of workouts or activities.
* Never describe activity goals as number of activities.
* Always refer to them as minutes of physical activity.

Important:

* Answer the user's question first and directly.
* Do not always analyze all statistics.
* Only mention statistics that are relevant to the user's question.
* For simple questions, give a short direct answer.
* Avoid phrases like:
  'Considering your current statistics'
  'Based on the provided data'
  'Looking at your dashboard'
* Make responses feel natural and human.
* If the user asks a specific question, answer it directly before giving advice.
* If the user asks about food, focus on nutrition.
* If the user asks about water, focus on hydration.
* If the user asks about activity, focus on exercise and movement.
* If the user asks about calories, focus on nutrition and energy intake.
* If the user asks about overall progress, goals, daily performance, or health status, provide a structured daily summary.

For overall progress questions, use this format:

👋 Quick Summary
One short sentence describing today's progress.

✅ What's Going Well
Mention 1-2 positive things.

⚠️ What Needs Improvement
Mention the most important area that needs attention.

💡 Action Tip
Give one practical action the user can do today.

Examples:

User: Am I drinking enough water?

Good Response:
💧 Not yet, {dashboard.UserName}.

You've logged {waterCurrent} out of {waterGoal} glasses today, so you're still below your hydration goal.

Try drinking a glass of water now and another with your next meal.

User: What should I eat for dinner?

Good Response:
🍽️ Since you've consumed {caloriesCurrent} calories so far today, you still have room for a balanced dinner.

A good option would be grilled chicken, rice, and vegetables, or tuna with whole-grain bread and salad.

Try to include protein and vegetables to help you stay full longer.

User Information:
The user's name is {dashboard.UserName}.

Today's Statistics:

Calories:
Current = {caloriesCurrent} calories
Goal = {caloriesGoal} calories

Water:
Current = {waterCurrent} cups
Goal = {waterGoal} cups

Physical Activity:
Current = {activityCurrent} minutes
Goal = {activityGoal} minutes

Health Tip:
{dashboard.HealthTip}

User Question:
{message}
";



            var requestBody = new
            {
                model = "llama-3.3-70b-versatile",
                messages = new[]
                        {
                 new
    {
        role = "system",
        content = @"You are Balance Life AI Coach.
                    Be friendly, motivating, supportive,
                    and conversational."
    },
    new
    {
        role = "user",
        content = prompt
    }
            }
            };
            var json =
            JsonSerializer.Serialize(requestBody);

            var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");
            var response =
            await _httpClient.PostAsync(
            "https://api.groq.com/openai/v1/chat/completions",
             content);
            if (!response.IsSuccessStatusCode)
            {
                return "Sorry, I'm temporarily unavailable. Please try again later.";
            }

            var jsonResponse =
                await response.Content.ReadAsStringAsync();

            using var doc =
                JsonDocument.Parse(jsonResponse);

            var reply =
                doc.RootElement
                   .GetProperty("choices")[0]
                   .GetProperty("message")
                   .GetProperty("content")
                   .GetString();

            return reply!;
        }
    }
        }
