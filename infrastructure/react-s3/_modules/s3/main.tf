#create an S3 bucket for the static site
resource "aws_s3_bucket" "static_site" {
    bucket = "${var.site_name}-personal-site-react-s3-${var.env}"
}

resource "aws_s3_bucket_policy" "allow_public_access" {
    bucket = aws_s3_bucket.static_site.id
    policy = data.aws_iam_policy_document.static_site_bucket_policy.json

}

#create the static site bucket policy
data "aws_iam_policy_document" "static_site_bucket_policy" {
    statement {
        actions = [
            "s3:GetObject"
        ]
        resources = [
            "${aws_s3_bucket.static_site.arn}/*"
        ]
        principals {
            type = "AWS"
            identifiers = [
                "*"
            ]
        }
    }
}