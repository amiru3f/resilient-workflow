package main

import (
	"context"
	"os/exec"

	"go.temporal.io/sdk/activity"
	"go.temporal.io/sdk/workflow"
)

func SendPushNotificationWorkflow(ctx workflow.Context, slackId string, content string) error {
	return nil
}

func NotifySlack(ctx context.Context, slackId string, content string) (string, error) {
	logger := activity.GetLogger(ctx)
	logger.Info("Activity", "name", content)

	exec.Command("terraform", "apply")

	return "Hello " + content + "!", nil
}
