export default function CommentCard({ comment }) {
    return (
        <div className="flex gap-3 bg-white p-3 rounded-lg border border-gray-700">
            <img
                src={comment.avatarUrl}
                alt={comment.username}
                className="w-8 h-8 rounded-full"
            />
            <div>
                <p className="text-sm font-semibold">{comment.username}</p>
                <p className="text-gray-300 text-sm">{comment.content}</p>
            </div>
        </div>
    );
}